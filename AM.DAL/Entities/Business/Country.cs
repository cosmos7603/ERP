using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Country : AuditableEntity
    {
        [Key]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryCodeAlpha2 { get; set; }
        public string CountryCodeNumeric { get; set; }
    }
}
