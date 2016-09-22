using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class IATA : AuditableEntity
    {
        [Key]
        public int IataId { get; set; }
        public string IataCode { get; set; }
        public bool HeadOffice { get; set; }
        public string PhoneNumber { get; set; }
        public string SabrePcc { get; set; }
    }
}
