using DataGenerator.Business.Infrastructure;
using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using DataGenerator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    public class PersonDataGenerator
    {
        private readonly PersonDataGeneratorSettings _settings;

        private PersonDataGenerator() { }

        public PersonDataGenerator(PersonDataGeneratorSettings settings)
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

        public string GetName(List<ILocalizableValue> items, IsoCode isoCode)
        {
            return _settings.Generator.Get(items, isoCode).ToString();
        }

        private Gender GetRandomGender()
        {
            return (Gender)new Random().Next(1);
        }
    }
}
