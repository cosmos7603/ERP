using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class OtherType : AuditableEntity
    {
        [Key]
        public int OtherTypeId { get; set; }
        public int StoreId { get; set; }
        public string OtherTypeName { get; set; }
        public string AgressoAccount { get; set; }
        public string AgressoType { get; set; }
        public bool Deleted { get; set; }
    }
}
