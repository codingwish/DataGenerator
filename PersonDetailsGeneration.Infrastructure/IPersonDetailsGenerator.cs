using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonDetailsGeneration.Infrastructure
{
    public interface IPersonDetailsGenerator
    {
        PersonDetail Get(IsoCode isoCode, Gender? gender);
        string Get(List<ICultureValue> items, IsoCode isoCode);
    }
}