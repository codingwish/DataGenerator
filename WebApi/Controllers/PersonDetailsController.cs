using DataGenerator.Core.Container;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonDetailsController : ControllerBase
    {
        private readonly ILogger<PersonDetailsController> _logger;
        private readonly IConfiguration _configuration;

        public PersonDetailsController(ILogger<PersonDetailsController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }

        [HttpGet]
        public IEnumerable<PersonDetail> Get(IsoCode isoCode, int count)
        {
            var dataLayer = ComponentFactory.CreateDataLayer();
            if (!dataLayer.Connect(_configuration.GetConnectionString("db")))
            {
                return null;
            }
            var result = new List<PersonDetail>();
            var options = ComponentFactory.CreatePersonDetailsGeneratorOptions
                (
                    ComponentFactory.CreateCultureValueGenerator(),
                    dataLayer.SelectRecords<MaleName>("MaleName").Result.ToList<ICultureValue>(),
                    dataLayer.SelectRecords<FemaleName>("FemaleName").Result.ToList<ICultureValue>(),
                    dataLayer.SelectRecords<LastName>("LastName").Result.ToList<ICultureValue>()
                );
            var generator = ComponentFactory.CreatePersonDetailsGenerator(options);
            for (int i = 0; i < count; i++)
            {
                result.Add(generator.Get(isoCode, null));
            }
            return result;
        }
    }
}

