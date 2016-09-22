using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Code : AuditableEntity
    {
        [Key]
        public int CodeId { get; set; }
        public int StoreId { get; set; }
        public string CodeTypeCode { get; set; }
        public string CodeName { get; set; }
        public bool DefaultCode { get; set; }
        public bool Deleted { get; set; }
        public string CodeLevelCode { get; set; }
        public string HoCode { get; set; }
    }
}
