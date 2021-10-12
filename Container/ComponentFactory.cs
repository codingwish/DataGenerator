using DataGenerator.Business;
using DataGenerator.Business.CultureValueGeneration;
using DataGenerator.Business.CultureValueGeneration.Infrastructure;
using DataGenerator.Business.PersonalDataGeneration;
using DataGenerator.Business.PersonalDataGeneration.Infrastructure;
using DataGenerator.Business.SampleData.Infrastructure;
using DataGenerator.Data.DataAccess;
using DataGenerator.Data.DataAccess.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Core.Container
{
    public class ComponentFactory
    {

        public IDataLayer CreateDataLayer() => new CosmosDataLayer();

        public IDataFileReader CreateDataFileReader() => new DataFileReader();

        public ISampleDataService CreateSampleDataService(IDataLayer dataLayer) => new SampleDataService(dataLayer);

        public IPersonDataGenerator CreatePersonalDataGenerator(IPersonDataGeneratorOptions settings) => new PersonDataGenerator(settings);

        public IPersonDataGeneratorOptions CreatePersonalDataGeneratorOptions
            (
                ICultureValueGenerator generator, 
                List<ICultureValue> lastNames,
                List<ICultureValue> maleNames,
                List<ICultureValue> femaleNames
            ) => new PersonDataGeneratorOptions(generator, lastNames, maleNames, femaleNames);

        public ICultureValueGenerator CreateCultureValueGenerator() => new CultureValueGenerator();
    }
}
