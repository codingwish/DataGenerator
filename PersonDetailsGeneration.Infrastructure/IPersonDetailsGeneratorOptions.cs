using DataGenerator.Business.CultureValueGeneration.Infrastructure;
using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.PersonDetailsGeneration.Infrastructure
{
    public interface IPersonDetailsGeneratorOptions
    {
        List<ICultureValue> FemaleNames { get; }
        ICultureValueGenerator Generator { get; }
        List<ICultureValue> LastNames { get; }
        List<ICultureValue> MaleNames { get; }
    }
}