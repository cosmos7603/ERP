using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerCC : AuditableEntity
    {
        [Key]
        public int CustomerCcId { get; set; }
        public int CustomerId { get; set; }
        public int CcTypeCode { get; set; }
        public string CcNum { get; set; }
        public string CcExpDate { get; set; }
        public bool? DefaultItem { get; set; }
        public string CcAddress { get; set; }
    }
}
