using DataGenerator.Data.Infrastructure;
using Newtonsoft.Json;
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
    public class LastName : ILocalizableValue
    {
        /// <inheritdoc />
        public string Value { get; set; }

        /// <inheritdoc />
        public int IsoCode { get; set; }
    }
}
