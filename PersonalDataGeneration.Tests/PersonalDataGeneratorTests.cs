using DataGenerator.Business.PersonalDataGeneration.Infrastructure;
using DataGenerator.Core.Container;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataGenerator.Business.PersonalDataGeneration.Tests
{
    public class PersonalDataGeneratorTests
    {
        
        [Fact]
        public void GetFullName_ShouldCreateFirstAndLastNameMale()
        {
            // Arrange
            var generator = new PersonDataGenerator(CreateOptions());
            string expected = "Benjamin Müller";

            // Act
            object actual = generator.GetFullName(IsoCode.DE, Gender.Male);

            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        [Fact]
        public void GetFullName_ShouldCreateFirstAndLastNameFemale()
        {
            // Arrange
            var generator = new PersonDataGenerator(CreateOptions());
            string expected = "Kim Smith";

            // Act
            object actual = generator.GetFullName(IsoCode.EN, Gender.Female);

            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        private List<ICultureValue> CreateLastNames()
        {
            return new List<ICultureValue>()
            {
                new LastName()
                {
                    IsoCode = (int)IsoCode.DE,
                    Value = "Müller"
                },
                new LastName()
                {
                    IsoCode = (int)IsoCode.EN,
                    Value = "Smith"
                }
            };
        }

        private List<ICultureValue> CreateFirstNamesMale()
        {
            return new List<ICultureValue>()
            {
                new MaleName()
                {
                    IsoCode = (int)IsoCode.DE,
                    Value = "Benjamin"
                },
                new MaleName()
                {
                    IsoCode = (int)IsoCode.EN,
                    Value = "Jon"
                }
            };
        }

        private List<ICultureValue> CreateFirstNamesFemale()
        {
            return new List<ICultureValue>()
            {
                new MaleName()
                {
                    IsoCode = (int)IsoCode.DE,
                    Value = "Laura"
                },
                new MaleName()
                {
                    IsoCode = (int)IsoCode.EN,
                    Value = "Kim"
                }
            };
        }

        private IPersonDataGeneratorOptions CreateOptions()
        {
            return new PersonDataGeneratorOptions
                (
                    ComponentFactory.CreateCultureValueGenerator(),
                    CreateFirstNamesMale(),
                    CreateFirstNamesFemale(),
                    CreateLastNames()
                );
        }
    }
}
