using DataGenerator.Data.Infrastructure;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataAccess
{
    /// <summary>
    /// Data access layer for a sql (core) cosmos db.
    /// </summary>
    public class CosmosDataLayer : IDataLayer
    {
        private Database _dataBase;

        /// <inheritdoc />
        public bool Connect(string connectionString)
        {
            try
            {
                var client = new CosmosClient(connectionString);
                _dataBase = client.CreateDatabaseIfNotExistsAsync("data").Result;
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <inheritdoc />
        public async Task InsertRecord<T>(string containerName, T item)
        {
            try
            {
                var container = await GetContainerAsync(containerName);
                var cosmosDbItem = CreateCosmosDbItem(item);
                await container.CreateItemAsync(cosmosDbItem);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> SelectRecords<T>(string containerName)
        {
            var result = new List<T>();
            var container = await GetContainerAsync(containerName);
            var queryResult = container.GetItemLinqQueryable<T>(true);
            foreach (var item in queryResult)
            {
                result.Add(item);
            }
            return result;
        }

        /// <inheritdoc />
        public async Task DeleteRepository()
        {
            await _dataBase.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task Delete(string containerName)
        {
            var container = await GetContainerAsync(containerName);
            await container.DeleteContainerAsync();
            Console.WriteLine($"{containerName} deleted");
            await GetContainerAsync(containerName);
        }

        /// <inheritdoc />
        public async Task Delete<T>(string containerName, object key)
        {
            var container = await GetContainerAsync(containerName);
            await container.DeleteItemAsync<T>(key.ToString(), new PartitionKey("/id"));
            Console.WriteLine($"{containerName} Item {key} deleted");
        }

        private async Task<Container> GetContainerAsync(string containerName)
        {
            return await _dataBase.CreateContainerIfNotExistsAsync(containerName, "/id");
        }

        /// <summary>
        /// Adds the mandatory id value as Guid to items.
        /// CosmosDb has no auto-increment on id, but id is mandatory.
        /// </summary>
        /// <typeparam name="T">Data model class.</typeparam>
        /// <param name="item">Item without mandatory id.</param>
        /// <returns>Item with id added.</returns>
        private object CreateCosmosDbItem<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            string jsonItem = JsonConvert.SerializeObject(item);
            jsonItem = jsonItem.Insert(1, $@"""id"":""{Guid.NewGuid()}"",");
            return JsonConvert.DeserializeObject(jsonItem);
        }
    }
}
