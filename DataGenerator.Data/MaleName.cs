using DataGenerator.Data.DataModels.Infrastructure;

namespace DataGenerator.Data.DataModels
{
    /// <summary>
    /// Male names data model.
    /// </summary>
    public class MaleName : ICultureValue
    {
        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
        public int IsoCode { get; set; }
    }
}
