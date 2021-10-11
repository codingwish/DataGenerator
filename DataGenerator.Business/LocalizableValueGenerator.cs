using DataGenerator.Business.Infrastructure;
using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGenerator.Business
{
    /// <summary>
    /// Gets a random value from a list of localizable items.
    /// </summary>
    public class LocalizableValueGenerator : ILocalizableValueGenerator
    {
        /// <summary>
        /// Internal constructor.
        /// </summary>
        public LocalizableValueGenerator() { }

        /// <summary>
        /// Gets a random value from the data.
        /// </summary>
        /// <returns>Random value.</returns>
        public object Get(List<ILocalizableValue> data)
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
        public object Get(List<ILocalizableValue> data, IsoCode isoCode)
        {
            IEnumerable<ILocalizableValue> languageItems = data.Where(d => d.IsoCode == (int)isoCode);
            return Get(languageItems.ToList());
        }
    }
}
