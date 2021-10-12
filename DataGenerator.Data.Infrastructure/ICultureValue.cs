namespace DataGenerator.Data.DataModels.Infrastructure
{
    /// <summary>
    /// Provides a value for a specific culture IsoCode.
    /// </summary>
    public interface ICultureValue
    {
        /// <summary>
        /// Culture iso code.
        /// </summary>
        int IsoCode { get; set; }

        /// <summary>
        /// Localized Value.
        /// </summary>
        string Value { get; set; }
    }
}