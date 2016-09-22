using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CabinSize : AuditableEntity
    {
        [Key]
        public string CabinSizeCode { get; set; }
        public string CabinSizeName { get; set; }
    }
}
