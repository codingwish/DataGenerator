using DataGenerator.Data.DataModels.Infrastructure;

namespace DataGenerator.Data.DataModels
{
    /// <summary>
    /// Persons details.
    /// </summary>
    public class PersonDetail
    {
        /// <summary>
        /// Persons first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Persons last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Persons country iso-code.
        /// </summary>
        public IsoCode IsoCode { get; set; }

        /// <summary>
        /// Persons gender.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
