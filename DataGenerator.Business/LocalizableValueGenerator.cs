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
    public class LocalizableValueGenerator
    {
        private readonly List<ILocalizableValue> _data;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        private LocalizableValueGenerator() { }

        /// <summary>
        /// Creates a new instance with the values provided.
        /// </summary>
        /// <param name="data">List of localizbable values.</param>
        public LocalizableValueGenerator(List<ILocalizableValue> data)
        {
            _data = data;
        }

        /// <summary>
        /// Gets a random value from the data.
        /// </summary>
        /// <returns>Random value.</returns>
        public object Get()
        {
            return Get(_data);
        }

        /// <summary>
        /// Gets a random value from the data for the specified language.
        /// </summary>
        /// <param name="isoCode">Language IsoCode.</param>
        /// <returns>Random value.</returns>
        public object Get(IsoCode isoCode)
        {
            IEnumerable<ILocalizableValue> languageItems = _data.Where(d => d.IsoCode == (int)isoCode);
            return Get(languageItems.ToList());
        }

        private object Get(List<ILocalizableValue> data)
        {
            if (data == null || data.Count <= 0)
            {
                return null;
            }
            return data[new Random().Next(data.Count)];
        }
    }
}
