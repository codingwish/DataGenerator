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
    public class SampleDataManager
    {
        private readonly IDataLayer _dataLayer;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        private SampleDataManager() { }

        public SampleDataManager(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            Reset();
            Load();
        }

        private void Load()
        {
            foreach (IsoCode isoCode in Enum.GetValues(typeof(IsoCode)))
            {
                CreateSampleDataItems<LastName>("LastNames", isoCode);
                CreateSampleDataItems<MaleName>("FirstNamesMale", isoCode);
                CreateSampleDataItems<FemaleName>("FirstNamesFemale", isoCode);
            }
        }

        private void Reset()
        {
            _dataLayer.DeleteRepository();
        }

        private void CreateSampleDataItems<T>(string fileName, IsoCode isoCode)
            where T : ILocalizedValue
        {
            var fileReader = new SampleDataFileReader();
            var sampleData = fileReader.Read($"{fileName}_{isoCode}.txt");
            if (sampleData == null)
            {
                return;
            }
            foreach (var value in sampleData)
            {
                var item = (ILocalizedValue)Activator.CreateInstance(typeof(T));
                item.IsoCode = (int)isoCode;
                item.Value = value.ToString();
                _dataLayer.InsertRecord(item.GetType().Name, item);
            }
        }
    }
}
