using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GSTPymt : AuditableEntity
    {
        [Key]
        public int GstPymtId { get; set; }
        public int? ResGrpId { get; set; }
        public bool? Grp { get; set; }
        public int? VendorId { get; set; }
        public string CurrencyCode { get; set; }
        public int? LineItemSeq { get; set; }
        public string ProductTypeCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaymentAmt { get; set; }
        public bool GstPymtRuleIgnored { get; set; }
        public string BankingBatchCode { get; set; }
        public int StoreId { get; set; }
        public bool Centralized { get; set; }
        public int? BankAccountId { get; set; }
        public bool Returned { get; set; }
        public bool CentralizedPymt { get; set; }
        public int? ReconciliationSetupId { get; set; }
    }
}
