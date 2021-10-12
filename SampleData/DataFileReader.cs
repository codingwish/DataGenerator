using DataGenerator.Business.SampleData.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    /// <summary>
    /// Provides methods to load data from a file.
    /// </summary>
    public class DataFileReader : IDataFileReader
    {
        /// <summary>
        /// Creates a new instance of the data fíle reader.
        /// </summary>
        public DataFileReader() { }

        /// <inheritdoc />
        public async Task<List<object>> ReadAsync(string fileName)
        {
            if (FileExists(fileName))
            {
                return null;
            }
            var lines = await File.ReadAllLinesAsync(fileName);
            return lines.ToList<object>();
        }

        /// <inheritdoc />
        public List<object> Read(string fileName)
        {
            if (FileExists(fileName))
            {
                return null;
            }
            var lines = File.ReadAllLines(fileName);
            return lines.ToList<object>();
        }

        private bool FileExists(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException($"{fileName} not found.");
                }
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
