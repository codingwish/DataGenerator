using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.Models
{
    /// <summary>
    /// Last names data model.
    /// </summary>
    public class LastName
    {
        /// <summary>
        /// Localized name.
        /// </summary>
        public string  Name { get; set; }

        /// <summary>
        /// Language iso code.
        /// </summary>
        public int IsoCode { get; set; }
    }
}
