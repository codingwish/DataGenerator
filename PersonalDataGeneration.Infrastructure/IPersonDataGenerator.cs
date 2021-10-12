using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonalDataGeneration.Infrastructure
{
    public interface IPersonDataGenerator
    {
        string GetFullName(IsoCode isoCode, Gender? gender);
        string GetName(List<ICultureValue> items, IsoCode isoCode);
    }
}