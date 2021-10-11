using DataGenerator.Data;
using DataGenerator.Data.Infrastructure;
using System.Collections.Generic;

namespace DataGenerator.Business.Infrastructure
{
    public interface ILocalizableValueGenerator
    {
        object Get(List<ILocalizableValue> data);
        object Get(List<ILocalizableValue> data, IsoCode isoCode);
    }
}