using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InsuranceEngine
    {
        [Key]
        public string Insuranceenginecode { get; set; }
        public string Insuranceenginename { get; set; }
        public string Systemkey { get; set; }
        public string Getquoteurl { get; set; }
        public string Bookquoteurl { get; set; }
        public string Getquoteencoding { get; set; }
        public string Bookquoteencoding { get; set; }
        public bool? Cvv { get; set; }
        public bool? Phone { get; set; }
    }
}
