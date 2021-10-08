using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.Infrastructure
{
    /// <summary>
    /// Data access layer.
    /// </summary>
    public interface IDataLayer
    {
        /// <summary>
        /// Establishes a connection to a repository.
        /// </summary>
        /// <param name="connectionString">Connection string to connect to the repository.</param>
        /// <returns>Connection successfull.</returns>
        bool Connect(string connectionString);

        /// <summary>
        /// Adds a new item to the repository.
        /// </summary>
        /// <typeparam name="T">Type of data model.</typeparam>
        /// <param name="container">Name of the container.</param>
        /// <param name="item">Item which should be added to the repository.</param>
        Task InsertRecord<T>(string container, T item);

        /// <summary>
        /// Loads a list of items from the repository.
        /// </summary>
        /// <typeparam name="T">Type of data model.</typeparam>
        /// <param name="container">Name of the container.</param>
        /// <returns></returns>
        Task<List<T>> SelectRecords<T>(string container);
    }
}
