using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Currency : AuditableEntity
    {
        [Key]
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySign { get; set; }
    }
}
