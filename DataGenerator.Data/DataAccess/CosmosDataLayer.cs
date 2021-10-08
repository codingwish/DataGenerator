﻿using DataGenerator.Data.Infrastructure;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
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
            try
            {
                Container container = await GetContainerAsync(containerName);
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

        /// <summary>
        /// Adds the mandatory id value as Guid to items.
        /// CosmosDb has no auto-increment on id, but id is mandatory.
        /// </summary>
        /// <typeparam name="T">Data model class.</typeparam>
        /// <param name="item">Item which needs to be converted to a valid item.</param>
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
