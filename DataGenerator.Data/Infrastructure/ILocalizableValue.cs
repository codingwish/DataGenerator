namespace DataGenerator.Data.Infrastructure
{
    /// <summary>
    /// Provides a value for a specific language IsoCode.
    /// </summary>
    public interface ILocalizableValue
    {
        /// <summary>
        /// Language iso code.
        /// </summary>
        int IsoCode { get; set; }

        /// <summary>
        /// Localized Value.
        /// </summary>
        string Value { get; set; }
    }
}