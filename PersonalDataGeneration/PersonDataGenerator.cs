using DataGenerator.Business.PersonalDataGeneration.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonalDataGeneration
{
    public class PersonDataGenerator : IPersonDataGenerator
    {
        private readonly IPersonDataGeneratorOptions _settings;

        private PersonDataGenerator() { }

        public PersonDataGenerator(IPersonDataGeneratorOptions settings)
        {
            _settings = settings;
        }

        public string GetFullName(IsoCode isoCode, Gender? gender)
        {
            if (gender == null || gender == Gender.Diverse)
            {
                gender = GetRandomGender();
            }
            string result = string.Empty;
            if (gender == Gender.Male)
            {
                result += GetName(_settings.MaleNames, isoCode);
            }
            else if (gender == Gender.Female)
            {
                result += GetName(_settings.FemaleNames, isoCode);
            }
            result += $" {GetName(_settings.LastNames, isoCode)}";
            return result;
        }

        public string GetName(List<ICultureValue> items, IsoCode isoCode)
        {
            return _settings.Generator.Get(items, isoCode).ToString();
        }

        private Gender GetRandomGender()
        {
            return (Gender)new Random().Next(1);
        }
    }
}
