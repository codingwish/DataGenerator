using DataGenerator.Business.PersonDetailsGeneration.Infrastructure;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonDetailsGeneration
{
    public class PersonDetailsGenerator : IPersonDetailsGenerator
    {
        private readonly IPersonDetailsGeneratorOptions _settings;

        private PersonDetailsGenerator() { }

        public PersonDetailsGenerator(IPersonDetailsGeneratorOptions settings)
        {
            _settings = settings;
        }

        public PersonDetail Get(IsoCode isoCode, Gender? gender)
        {
            if (gender == null || gender == Gender.Diverse)
            {
                gender = GetRandomGender();
            }
            var result = new PersonDetail();
            result.Gender = (Gender)gender;
            if (gender == Gender.Male)
            {
                result.FirstName = Get(_settings.MaleNames, isoCode);
            }
            else if (gender == Gender.Female)
            {
                result.FirstName = Get(_settings.FemaleNames, isoCode);
            }
            result.LastName = Get(_settings.LastNames, isoCode);
            result.IsoCode = isoCode;
            return result;
        }

        public string Get(List<ICultureValue> items, IsoCode isoCode)
        {
            return _settings.Generator.Get(items, isoCode).ToString();
        }

        private Gender GetRandomGender()
        {
            return (Gender)new Random().Next(2);
        }
    }
}
