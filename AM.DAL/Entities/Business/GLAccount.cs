using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GLAccount : AuditableEntity
    {
        [Key]
        public int GlAccountId { get; set; }
        public string GlAccountName { get; set; }
        public string ListCode { get; set; }
        public string CodeLevelCode { get; set; }
        public string HoCode { get; set; }
        public int? StoreId { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultItem { get; set; }
    }
}
