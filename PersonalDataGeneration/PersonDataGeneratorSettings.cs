using DataGenerator.Business.CultureValueGeneration.Infrastructure;
using DataGenerator.Business.PersonalDataGeneration.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonalDataGeneration
{
    public class PersonDataGeneratorOptions : IPersonDataGeneratorOptions
    {
        public ICultureValueGenerator Generator { get; private set; }

        public List<ICultureValue> MaleNames { get; private set; }

        public List<ICultureValue> FemaleNames { get; private set; }

        public List<ICultureValue> LastNames { get; private set; }

        public PersonDataGeneratorOptions(
            ICultureValueGenerator generator,
            List<ICultureValue> maleNames,
            List<ICultureValue> femaleNames,
            List<ICultureValue> lastNames
            )
        {
            Generator = generator;
            MaleNames = maleNames;
            FemaleNames = femaleNames;
            LastNames = lastNames;
        }
    }
}
