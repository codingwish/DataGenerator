using DataGenerator.Data.DataModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator.Data.DataModels
{
    public class PersonDetail
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IsoCode IsoCode { get; set; }

        public Gender Gender { get; set; }
    }
}
