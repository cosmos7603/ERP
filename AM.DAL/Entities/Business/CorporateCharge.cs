using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CorporateCharge : AuditableEntity
    {
        [Key]
        public int CorporateChargeId { get; set; }
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal ChargeAmt { get; set; }
        public decimal PaidAmt { get; set; }
        public string ChargeTypeCode { get; set; }
        public int ReasonId { get; set; }
        public string ChargeComments { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("ReasonId")]
        public CorporateChargeReason CorporateChargeReason { get; set; }
    }
}
