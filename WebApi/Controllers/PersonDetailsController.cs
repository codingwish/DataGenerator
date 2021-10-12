using DataGenerator.Core.Container;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    /// <summary>
    /// Rest-Api providing a list of person details.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonDetailsController : ControllerBase
    {
        private readonly ILogger<PersonDetailsController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="configuration">Configuration</param>
        public PersonDetailsController(ILogger<PersonDetailsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Get a list of random person details.
        /// </summary>
        /// <param name="isoCode">IsoCode of persons country.
        /// 0=EN
        /// 1=DE
        /// 2=FR
        /// 3=JP
        /// 4=RU
        /// </param>
        /// <param name="count">Number of items to be created.</param>
        /// <returns>A list of person details.</returns>
        [HttpGet]
        public IEnumerable<PersonDetail> Get(IsoCode isoCode, int count)
        {
            DataGenerator.Data.DataAccess.Infrastructure.IDataLayer dataLayer = ComponentFactory.CreateDataLayer();
            if (!dataLayer.Connect(_configuration.GetConnectionString("db")))
            {
                _logger.LogError("No database connection");
                throw new ApplicationException("No database connection");
            }
            List<PersonDetail> result = new List<PersonDetail>();
            DataGenerator.Business.PersonDetailsGeneration.Infrastructure.IPersonDetailsGeneratorOptions options = ComponentFactory.CreatePersonDetailsGeneratorOptions
                (
                    ComponentFactory.CreateCultureValueGenerator(),
                    dataLayer.SelectRecords<MaleName>("MaleName").Result.ToList<ICultureValue>(),
                    dataLayer.SelectRecords<FemaleName>("FemaleName").Result.ToList<ICultureValue>(),
                    dataLayer.SelectRecords<LastName>("LastName").Result.ToList<ICultureValue>()
                );
            DataGenerator.Business.PersonDetailsGeneration.Infrastructure.IPersonDetailsGenerator generator = ComponentFactory.CreatePersonDetailsGenerator(options);
            for (int i = 0; i < count; i++)
            {
                result.Add(generator.Get(isoCode, null));
            }
            return result;
        }
    }
}

