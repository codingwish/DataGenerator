using DataGenerator.Business.PersonDetailsGeneration.Infrastructure;
using DataGenerator.Core.Container;
using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataGenerator.Business.PersonDetailsGeneration.Tests
{
    public class PersonalDataGeneratorTests
    {
        
        [Fact]
        public void Get_ShouldCreateMalePerson()
        {
            // Arrange
            var generator = new PersonDetailsGenerator(CreateOptions());
            var expected = new PersonDetail()
            {
                FirstName = "Benjamin",
                LastName = "Müller",
                IsoCode = IsoCode.DE,
                Gender = Gender.Male
            };

            // Act
            var actual = generator.Get(IsoCode.DE, Gender.Male);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
        }

        [Fact]
        public void GetFullName_ShouldCreateFemalePerson()
        {
            // Arrange
            var generator = new PersonDetailsGenerator(CreateOptions());
            var expected = new PersonDetail()
            {
                FirstName = "Kim",
                LastName = "Smith",
                IsoCode = IsoCode.EN,
                Gender = Gender.Female
            };

            // Act
            var actual = generator.Get(IsoCode.EN, Gender.Female);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(actual));
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

        private IPersonDetailsGeneratorOptions CreateOptions()
        {
            return new PersonDetailsGeneratorOptions
                (
                    ComponentFactory.CreateCultureValueGenerator(),
                    CreateFirstNamesMale(),
                    CreateFirstNamesFemale(),
                    CreateLastNames()
                );
        }
    }
}
