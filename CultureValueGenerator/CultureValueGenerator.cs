using DataGenerator.Business.CultureValueGeneration.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Business.CultureValueGeneration
{
    /// <summary>
    /// Gets a random value from a list of localizable items.
    /// </summary>
    public class CultureValueGenerator : ICultureValueGenerator
    {
        /// <summary>
        /// Creates a new instance of the generator.
        /// </summary>
        public CultureValueGenerator() { }

        /// <summary>
        /// Gets a random value from the data.
        /// </summary>
        /// <returns>Random value.</returns>
        public object Get(List<ICultureValue> data)
        {
            if (data == null || data.Count <= 0)
            {
                return null;
            }
            return data[new Random().Next(data.Count)].Value;
        }

        /// <summary>
        /// Gets a random value from the data for the specified language.
        /// </summary>
        /// <param name="isoCode">Language IsoCode.</param>
        /// <returns>Random value.</returns>
        public object Get(List<ICultureValue> data, IsoCode isoCode)
        {
            IEnumerable<ICultureValue> languageItems = data.Where(d => d.IsoCode == (int)isoCode);
            return Get(languageItems.ToList());
        }
    }
}
