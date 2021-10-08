using DataGenerator.Data.Infrastructure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataAccess
{
    public class MongoDataLayer : IDataLayer
    {
        private IMongoDatabase _dataBase;

        /// <inheritdoc />
        public bool Connect(string connectionString)
        {
            try
            {
                var client = new MongoClient(connectionString);
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
            var collection = _dataBase.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(item);
        }

        /// <inheritdoc />
        public async Task<List<T>> SelectRecords<T>(string collectionName)
        {
            var result = new List<T>();
            var collection = _dataBase.GetCollection<T>(collectionName);
            var queryResult = await Task.FromResult(collection.AsQueryable());
            foreach (var item in queryResult)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
