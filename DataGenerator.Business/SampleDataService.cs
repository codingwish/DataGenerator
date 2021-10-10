using DataGenerator.Business.Infrastructure;
using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using DataGenerator.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    /// <inheritdoc />
    public class SampleDataService : ISampleDataService
    {
        private readonly IDataLayer _dataLayer;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        private SampleDataService() { }

        /// <summary>
        /// Creates a new instance with the provided data layer.
        /// </summary>
        /// <param name="dataLayer">Data access layer.</param>
        public SampleDataService(IDataLayer dataLayer)
        {
            if (dataLayer == null)
            {
                throw new ArgumentNullException(nameof(dataLayer));
            }
            _dataLayer = dataLayer;
        }

        /// <inheritdoc />
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
                Debug.WriteLine($"{tasks.Count} remaining.");
            }
        }

        private async Task Reset()
        {
            await _dataLayer.Delete("LastName");
            await _dataLayer.Delete("MaleName");
            await _dataLayer.Delete("FemaleName");
        }

        private async Task CreateSampleDataItemsAsync<T>(SampleDataFileReader fileReader, string fileName, IsoCode isoCode)
            where T : ILocalizableValue
        {
            try
            {
                Debug.WriteLine($"{fileName} {isoCode} started");
                var sampleData = await fileReader.ReadAsync($"{fileName}_{isoCode}.txt");
                if (sampleData == null)
                {
                    return;
                }
                foreach (var value in sampleData)
                {
                    var item = (ILocalizableValue)Activator.CreateInstance(typeof(T));
                    item.IsoCode = (int)isoCode;
                    item.Value = value.ToString();
                    await _dataLayer.InsertRecord(item.GetType().Name, item);
                }
                Console.WriteLine($"{fileName} {isoCode} finished");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
