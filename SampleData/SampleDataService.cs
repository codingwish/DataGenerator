using DataGenerator.Business.SampleData.Infrastructure;
using DataGenerator.Data.DataAccess.Infrastructure;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task Init(IDataFileReader fileReader)
        {
            await Reset();
            await Load(fileReader);
        }

        private async Task Load(IDataFileReader fileReader)
        {
            List<Task> tasks = new List<Task>();
            foreach (IsoCode isoCode in Enum.GetValues(typeof(IsoCode)))
            {
                tasks.Add(CreateSampleDataItemsAsync<LastName>(fileReader, "LastNames", isoCode));
                tasks.Add(CreateSampleDataItemsAsync<MaleName>(fileReader, "FirstNamesMale", isoCode));
                tasks.Add(CreateSampleDataItemsAsync<FemaleName>(fileReader, "FirstNamesFemale", isoCode));
            }
            while (tasks.Count > 0)
            {
                Task task = await Task.WhenAny(tasks);
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

        private async Task CreateSampleDataItemsAsync<T>(IDataFileReader fileReader, string fileName, IsoCode isoCode)
            where T : ICultureValue
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
                    await CreateSampeDataItemAsync<T>(isoCode, value);
                }
                Console.WriteLine($"{fileName} {isoCode} finished");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task CreateSampeDataItemAsync<T>(IsoCode isoCode, object value)
            where T : ICultureValue
        {
            var item = (ICultureValue)Activator.CreateInstance(typeof(T));
            item.IsoCode = (int)isoCode;
            item.Value = value.ToString();
            await _dataLayer.InsertRecord(item.GetType().Name, item);
        }
    }
}
