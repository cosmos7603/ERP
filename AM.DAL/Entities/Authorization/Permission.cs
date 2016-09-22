using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Permission : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public string Login { get; set; }
        [Key, Column(Order = 1)]
        public string ResourceName { get; set; }
        public bool? AllowRead { get; set; }
        public bool? AllowCreate { get; set; }
        public bool? AllowUpdate { get; set; }
        [Column("AllowDeleted")]
        public bool? AllowDelete { get; set; }

        [ForeignKey("ResourceName")]
        public virtual Resource Resource { get; set; }
    }
}

