using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    /// <summary>
    /// Gets a random value from a list of localizable items.
    /// </summary>
    public class RandomLocalizableValueGenerator
    {
        private readonly List<ILocalizableValue> _data;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        private RandomLocalizableValueGenerator() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datalayer"></param>
        public RandomLocalizableValueGenerator(List<ILocalizableValue> data)
        {
            _data = data;
        }

        public object Get()
        {
            return Get(_data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isoCode"></param>
        /// <returns></returns>
        public object Get(IsoCode isoCode)
        {
            var languageItems = _data.Where(d => d.IsoCode == (int)isoCode);
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
