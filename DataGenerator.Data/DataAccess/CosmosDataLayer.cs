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
        public async Task InsertRecord<T>(string container, T item)
        {
            Container collection = await _dataBase.CreateContainerIfNotExistsAsync(container, "ID");
            await collection.CreateItemAsync(item);
        }

        /// <inheritdoc />
        public async Task<List<T>> SelectRecords<T>(string container)
        {
            var result = new List<T>();
            Container collection = await GetContainerAsync(container);
            var queryResult = collection.GetItemLinqQueryable<T>();
            foreach (var item in queryResult)
            {
                result.Add(item);
            }
            return result;
        }

        private async Task<Container> GetContainerAsync(string container)
        {
            return await _dataBase.CreateContainerIfNotExistsAsync(container, "ID");
        }
    }
}
