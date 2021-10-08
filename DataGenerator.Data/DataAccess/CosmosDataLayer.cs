using DataGenerator.Data.Infrastructure;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataAccess
{
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
            Container container = await _dataBase.CreateContainerIfNotExistsAsync(containerName, "/id");
            await container.CreateItemAsync(item);
        }

        /// <inheritdoc />
        public async Task<List<T>> SelectRecords<T>(string containerName)
        {
            var result = new List<T>();
            Container container = await GetContainerAsync(containerName);
            var queryResult = container.GetItemLinqQueryable<T>(true);
            foreach (var item in queryResult)
            {
                result.Add(item);
            }
            return result;
        }

        private async Task<Container> GetContainerAsync(string containerName)
        {
            return await _dataBase.CreateContainerIfNotExistsAsync(containerName, "/id");
        }
    }
}
