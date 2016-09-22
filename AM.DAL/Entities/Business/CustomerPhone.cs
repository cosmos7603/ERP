using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerPhone : AuditableEntity
    {
        [Key]
        public int CustomerPhoneId { get; set; }
        public int CustomerId { get; set; }
        public int PhoneTypeCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
