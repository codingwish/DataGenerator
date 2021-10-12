using DataGenerator.Data.DataModels.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.CultureValueGeneration.Infrastructure
{
    public interface ICultureValueGenerator
    {
        object Get(List<ICultureValue> data);
        object Get(List<ICultureValue> data, IsoCode isoCode);
    }
}