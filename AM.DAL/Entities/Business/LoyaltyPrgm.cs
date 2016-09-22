using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LoyaltyPrgm : AuditableEntity
    {
        [Key]
        public int LoyaltyPrgmId { get; set; }
        public int VendorId { get; set; }
        public string LoyaltyPrgmName { get; set; }
        public int LoyaltyPrgmSeq { get; set; }
        public bool Deleted { get; set; }
    }
}
