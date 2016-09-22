using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CorporateChargeReason : AuditableEntity
    {
        [Key]
        public int CorporateChargeReasonId { get; set; }
        public int? StoreId { get; set; }
        public string ChargeName { get; set; }
        public string ChargeTypeCode { get; set; }
        public bool Deleted { get; set; }
    }
}
