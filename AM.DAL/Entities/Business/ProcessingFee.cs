using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class ProcessingFee : AuditableEntity
    {
        [Key]
        public int ProcessingFeeId { get; set; }
        public string FeeTypeCode { get; set; }
        public decimal ProcessingFeeAmt { get; set; }
        public int? RevenuePymtId { get; set; }
        public bool Deleted { get; set; }
    }
}
