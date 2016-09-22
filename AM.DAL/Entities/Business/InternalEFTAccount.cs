using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class InternalEFTAccount : AuditableEntity
    {
        [Key]
        public int InternalEftAccountId { get; set; }
        public string InternalEftAccountName { get; set; }
        public string InternalEftAccountGl { get; set; }
        public string HoCode { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultItem { get; set; }
    }
}
