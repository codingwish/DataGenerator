using DataGenerator.Data.DataModels.Infrastructure;

namespace DataGenerator.Data.DataModels
{
    /// <summary>
    /// Female names data model.
    /// </summary>
    public class FemaleName : ICultureValue
    {
        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
        public int IsoCode { get; set; }
    }
}
