using DataGenerator.Data.DataModels.Infrastructure;

namespace DataGenerator.Data.DataModels
{
    /// <summary>
    /// Last names data model.
    /// </summary>
    public class LastName : ICultureValue
    {
        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
        public int IsoCode { get; set; }
    }
}
