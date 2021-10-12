using DataGenerator.Data.DataModels;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace DataGenerator.Business.CultureValueGeneration.Tests
{

    public class CultureValueGeneratorTests
    {
        [Fact]
        public void Get_ShouldReturnRandomValue()
        {
            // Arrange
            CultureValueGenerator generator = new CultureValueGenerator();


            // Act
            object actual = generator.Get(CreateData());

            // Assert
            Assert.Contains(actual.ToString(), "Müller, Smith");
        }

        [Fact]
        public void Get_ShouldReturnRandomCultureSpecificValue()
        {
            // Arrange
            CultureValueGenerator generator = new CultureValueGenerator();
            string expected = "Müller";

            // Act
            object actual = generator.Get(CreateData(), IsoCode.DE);

            // Assert
            Assert.Equal(expected, actual.ToString());
        }

        private List<ICultureValue> CreateData()
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
    }
}
