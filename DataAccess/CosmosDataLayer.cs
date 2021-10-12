using DataGenerator.Data.DataAccess.Infrastructure;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                CosmosClient client = new CosmosClient(connectionString);
                _dataBase = client.CreateDatabaseIfNotExistsAsync("data").Result;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

        /// <inheritdoc />
        public async Task InsertRecord<T>(string containerName, T item)
        {
            try
            {
                Container container = await GetContainerAsync(containerName);
                object cosmosDbItem = CreateCosmosDbItem(item);
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
            List<T> result = new List<T>();
            Container container = await GetContainerAsync(containerName);
            IOrderedQueryable<T> queryResult = container.GetItemLinqQueryable<T>(true);
            foreach (T item in queryResult)
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
        public async Task<bool> Delete(string containerName)
        {
            Container container = await GetContainerAsync(containerName);
            await container.DeleteContainerAsync();
            Console.WriteLine($"{containerName} deleted");
            await GetContainerAsync(containerName);
            return true;
        }

        /// <inheritdoc />
        public async Task Delete<T>(string containerName, object key)
        {
            Container container = await GetContainerAsync(containerName);
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
