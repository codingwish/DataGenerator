using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataAccess.Infrastructure
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
        /// <param name="table">Name of the container/collection/table.</param>
        /// <param name="entity">Item which should be added to the repository.</param>
        Task InsertRecord<T>(string table, T entity);

        /// <summary>
        /// Loads a list of items from the repository.
        /// </summary>
        /// <typeparam name="T">Type of data model.</typeparam>
        /// <param name="table">Name of the container/collection/table.</param>
        /// <returns>All items from the container/collection/table.</returns>
        Task<List<T>> SelectRecords<T>(string table);

        /// <summary>
        /// Deletes the repository.
        /// </summary>
        Task DeleteRepository();

        /// <summary>
        /// Deletes a table/container/collection from the repository.
        /// </summary>
        /// <param name="table">Name of the table.</param>
        Task<bool> Delete(string table);

        /// <summary>
        /// Deletes an item from the repository.
        /// </summary>
        /// <typeparam name="T">Type of data model.</typeparam>
        /// <param name="table">Name of the table.</param>
        /// <param name="key">Key of the item to be deleted.</param>
        Task Delete<T>(string table, object key);
    }
}
