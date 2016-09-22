using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Lead : AuditableEntity
    {
        [Key]
        public int LeadId { get; set; }
        public int StoreId { get; set; }
        public string StatusCode { get; set; }
        public int? DestinationId { get; set; }
        public short? DestAreaId { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string CounselorLogin { get; set; }
        public int? MktgSrcId { get; set; }
        public int? VendorId { get; set; }
        public bool Grp { get; set; }
        public DateTime? ReopenDate { get; set; }
        public DateTime? NextFollowupDate { get; set; }
        public bool AtRisk { get; set; }
        public int? ShipId { get; set; }
        public string Score { get; set; }
        public int? LeadRequestId { get; set; }

		public virtual List<LeadCustomer> LeadCustomers { get; set; } 

    }
}
