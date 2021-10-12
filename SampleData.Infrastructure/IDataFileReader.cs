using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataGenerator.Business.SampleData.Infrastructure
{
    /// <summary>
    /// Implements methods to load data from a file in SampleData directory.
    /// </summary>
    public interface IDataFileReader
    {
        /// <summary>
        /// Returns all lines of a file.
        /// </summary>
        /// <param name="fileName">Name of the file to read.</param>
        /// <returns>List of lines.</returns>
        List<object> Read(string fileName);

        /// <summary>
        /// Returns all lines of a file.
        /// </summary>
        /// <param name="fileName">Name of the file to read.</param>
        /// <returns>List of lines.</returns>
        Task<List<object>> ReadAsync(string fileName);
    }
}