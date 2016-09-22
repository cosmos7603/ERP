using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InsuranceEngineOtherUrls
    {
        [Key, Column(Order = 0)]
        public string InsuranceEngineCode { get; set; }
        [Key, Column(Order = 1)]
        public string CountryCode { get; set; }
        public string GetQuoteUrl { get; set; }
        public string BookQuoteUrl { get; set; }
    }
}
