using DataGenerator.Data.DataAccess.Infrastructure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataAccess
{
    /// <summary>
    /// Data access layer for a mongo (atlas) db.
    /// </summary>
    public class MongoDataLayer : IDataLayer
    {
        private IMongoDatabase _dataBase;

        /// <inheritdoc />
        public bool Connect(string connectionString)
        {
            try
            {
                MongoClient client = new MongoClient(connectionString);
                _dataBase = client.GetDatabase("data");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task InsertRecord<T>(string collectionName, T item)
        {
            IMongoCollection<T> collection = _dataBase.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(item);
        }

        /// <inheritdoc />
        public async Task<List<T>> SelectRecords<T>(string collectionName)
        {
            List<T> result = new List<T>();
            IMongoCollection<T> collection = _dataBase.GetCollection<T>(collectionName);
            MongoDB.Driver.Linq.IMongoQueryable<T> queryResult = await Task.FromResult(collection.AsQueryable());
            foreach (T item in queryResult)
            {
                result.Add(item);
            }
            return result;
        }

        /// <inheritdoc />
        public async Task DeleteRepository()
        {
            _dataBase.Client.DropDatabase("data");
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string collectionName)
        {
            _dataBase.DropCollection(collectionName);
            return true;
        }

        /// <inheritdoc />
        public async Task Delete<T>(string collectionName, object key)
        {
            IMongoCollection<T> collection = _dataBase.GetCollection<T>(collectionName);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", key);
            collection.FindOneAndDelete<T>(filter);
        }
    }
}
