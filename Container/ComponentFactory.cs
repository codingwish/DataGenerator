using DataGenerator.Business;
using DataGenerator.Business.CultureValueGeneration;
using DataGenerator.Business.CultureValueGeneration.Infrastructure;
using DataGenerator.Business.PersonDetailsGeneration;
using DataGenerator.Business.PersonDetailsGeneration.Infrastructure;
using DataGenerator.Business.SampleData.Infrastructure;
using DataGenerator.Data.DataAccess;
using DataGenerator.Data.DataAccess.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Core.Container
{
    public static class ComponentFactory
    {

        public static IDataLayer CreateDataLayer() => new CosmosDataLayer();

        public static IDataFileReader CreateDataFileReader() => new DataFileReader();

        public static ISampleDataService CreateSampleDataService(IDataLayer dataLayer) => new SampleDataService(dataLayer);

        public static IPersonDetailsGenerator CreatePersonDetailsGenerator(IPersonDetailsGeneratorOptions settings) => new PersonDetailsGenerator(settings);

        public static IPersonDetailsGeneratorOptions CreatePersonDetailsGeneratorOptions
            (
                ICultureValueGenerator generator, 
                List<ICultureValue> maleNames,
                List<ICultureValue> femaleNames,
                List<ICultureValue> lastNames
            ) => new PersonDetailsGeneratorOptions(generator, maleNames, femaleNames, lastNames);

        public static ICultureValueGenerator CreateCultureValueGenerator() => new CultureValueGenerator();
    }
}
