using DataGenerator.Business.Infrastructure;
using DataGenerator.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Business
{
    public class PersonDataGeneratorSettings
    {
        public ILocalizableValueGenerator Generator { get; private set; }

        public List<ILocalizableValue> MaleNames { get; private set; }

        public List<ILocalizableValue> FemaleNames { get; private set; }

        public List<ILocalizableValue> LastNames { get; private set; }

        public PersonDataGeneratorSettings(
            ILocalizableValueGenerator generator,
            List<ILocalizableValue> maleNames,
            List<ILocalizableValue> femaleNames,
            List<ILocalizableValue> lastNames
            ) 
        {
            Generator = generator;
            MaleNames = maleNames;
            FemaleNames = femaleNames;
            LastNames = lastNames;
        }
    }
}
