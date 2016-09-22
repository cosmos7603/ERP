using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PayType : AuditableEntity
    {
        [Key]
        public string PayTypeCode { get; set; }
        public string PayTypeDescr { get; set; }
        public bool? DbleEntry { get; set; }
    }
}
