using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpReg : AuditableEntity
    {
        [Key]
        public int GrpRegId { get; set; }
        public int GrpId { get; set; }
        public int GrpLinePricingId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public bool Pending { get; set; }
        public int? BerthOptionId { get; set; }
        public string RoommateFriendName { get; set; }
        public bool Declined { get; set; }
    }
}
