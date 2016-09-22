using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerLoyaltyPrgm
    {
        [Key]
        public int CustomerLoyaltyPrgmId { get; set; }
        public int CustomerId { get; set; }
        public int? LoyaltyPrgmId { get; set; }
        public string LoyaltyPrgmNumber { get; set; }
        public int VendorId { get; set; }
    }
}
