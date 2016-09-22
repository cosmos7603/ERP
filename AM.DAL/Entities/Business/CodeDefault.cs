using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CodeDefault : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int StoreId { get; set; }
        [Key, Column(Order = 1)]
        public string CodeTypeCode { get; set; }
        public int CodeId { get; set; }
    }
}
