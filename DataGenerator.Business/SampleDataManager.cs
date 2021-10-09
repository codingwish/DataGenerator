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
        public async Task Init()
        {
            await Reset();
            await Load();
        }

        private async Task Load()
        {
            var tasks = new List<Task>();
            var fileReader = new SampleDataFileReader();
            foreach (IsoCode isoCode in Enum.GetValues(typeof(IsoCode)))
            {
                tasks.Add(CreateSampleDataItemsAsync<LastName>(fileReader, "LastNames", isoCode));
                tasks.Add(CreateSampleDataItemsAsync<MaleName>(fileReader, "FirstNamesMale", isoCode));
                tasks.Add(CreateSampleDataItemsAsync<FemaleName>(fileReader, "FirstNamesFemale", isoCode));
            }
            while (tasks.Count > 0)
            {
                var task = await Task.WhenAny(tasks);
                tasks.Remove(task);
                Console.WriteLine($"{tasks.Count} remaining.");
            }
        }

        private async Task Reset()
        {
            var tasks = new List<Task>();
            tasks.Add(_dataLayer.Delete("LastName"));
            tasks.Add(_dataLayer.Delete("MaleName"));
            tasks.Add(_dataLayer.Delete("FemaleName"));
            await Task.WhenAll(tasks);
        }

        private async Task CreateSampleDataItemsAsync<T>(SampleDataFileReader fileReader, string fileName, IsoCode isoCode)
            where T : ILocalizedValue
        {
            try
            {
                Console.WriteLine($"{fileName} {isoCode} ready");
                var sampleData = await fileReader.ReadAsync($"{fileName}_{isoCode}.txt");
                if (sampleData == null)
                {
                    return;
                }
                foreach (var value in sampleData)
                {
                    var item = (ILocalizedValue)Activator.CreateInstance(typeof(T));
                    item.IsoCode = (int)isoCode;
                    item.Value = value.ToString();
                    await _dataLayer.InsertRecord(item.GetType().Name, item);
                }
                Console.WriteLine($"{fileName} {isoCode} finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
