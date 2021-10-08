using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataGenerator.Business
{
    /// <summary>
    /// Provides methods to load data from a file in SampleData directory.
    /// </summary>
    public class SampleDataFileReader
    {
        /// <summary>
        /// Returns all lines of a sample data file.
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Lines of file.</returns>
        public List<object> Read(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            string path = GetFilePath(fileName);
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            return File.ReadLines(path).ToList<object>();
        }

        private string GetFilePath(string fileName)
        {
            try
            {
                string path = $@"{Environment.CurrentDirectory}\SampleData\";
                if (!File.Exists($"{path}{fileName}"))
                {
                    throw new FileNotFoundException($"{path}{fileName} not found.");
                }
                return $"{path}{fileName}";
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
