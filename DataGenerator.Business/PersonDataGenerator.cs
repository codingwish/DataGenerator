using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using DataGenerator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    public class PersonDataGenerator
    {
        private readonly IDataLayer _dataLayer;

        private PersonDataGenerator() { }

        public PersonDataGenerator(IDataLayer dataLayer)
        {

        }

        public async Task<string> GetLastName(IsoCode isoCode)
        {
            var lastNames = await _dataLayer.SelectRecords<LastName>("LastName");
            var service = new LocalizableValueGenerator(lastNames.ToList<ILocalizableValue>());
            return service.Get(isoCode).ToString();
        }
    }
}
