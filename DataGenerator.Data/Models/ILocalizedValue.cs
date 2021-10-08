namespace DataGenerator.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalizedValue
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