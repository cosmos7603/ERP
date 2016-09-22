using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AgencyCardPymt : AuditableEntity
    {
        [Key]
        public int AgencyCardPymtId { get; set; }
        public string PayTypeCode { get; set; }
        public string CheckNum { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Reversed { get; set; }
        public int? ReconciliationSetupId { get; set; }
    }
}
