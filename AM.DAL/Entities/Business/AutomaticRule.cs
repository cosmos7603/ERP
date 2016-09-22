using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AutomaticRule : AuditableEntity
    {
        [Key]
        public int AutomaticRuleId { get; set; }
        public int StoreId { get; set; }
        public string AutomaticRuleTypeCode { get; set; }
        public short? RenewalMonth { get; set; }
        public decimal BillingAmt { get; set; }
        public short? FeeUserNumber { get; set; }
        public string CounselorLogin { get; set; }
        public bool Deleted { get; set; }
    }
}
