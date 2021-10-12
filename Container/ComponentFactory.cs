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
    /// <summary>
    /// DI factory returning implementations for interfaces.
    /// </summary>
    public static class ComponentFactory
    {
        /// <summary>
        /// Creates the data access layer.
        /// </summary>
        /// <returns>CosmosDataLayer</returns>
        public static IDataLayer CreateDataLayer() => new CosmosDataLayer();

        /// <summary>
        /// Creates the file reader for a sample data service.
        /// </summary>
        /// <returns>DataFileReader</returns>
        public static IDataFileReader CreateDataFileReader() => new DataFileReader();

        /// <summary>
        /// Creates a service to init sample data to a repository.
        /// </summary>
        /// <param name="dataLayer">Data access layer.</param>
        /// <returns>SampleDataService</returns>
        public static ISampleDataService CreateSampleDataService(IDataLayer dataLayer) => new SampleDataService(dataLayer);

        /// <summary>
        /// Creates a generator to build random person details.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IPersonDetailsGenerator CreatePersonDetailsGenerator(IPersonDetailsGeneratorOptions settings) => new PersonDetailsGenerator(settings);

        /// <summary>
        /// Sets the options for a person details generator.
        /// </summary>
        /// <param name="generator">Person details generator.</param>
        /// <param name="maleNames">List of male names.</param>
        /// <param name="femaleNames">List of female names.</param>
        /// <param name="lastNames">List of last names.</param>
        /// <returns>PersonDetailsGeneratorOptions</returns>
        public static IPersonDetailsGeneratorOptions CreatePersonDetailsGeneratorOptions
            (
                ICultureValueGenerator generator, 
                List<ICultureValue> maleNames,
                List<ICultureValue> femaleNames,
                List<ICultureValue> lastNames
            ) => new PersonDetailsGeneratorOptions(generator, maleNames, femaleNames, lastNames);

        /// <summary>
        /// Creates a generator to build randomized data for s specific culture.
        /// </summary>
        /// <returns>CultureValueGenerator</returns>
        public static ICultureValueGenerator CreateCultureValueGenerator() => new CultureValueGenerator();
    }
}
