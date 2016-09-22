using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Conglomerate : AuditableEntity
    {
        [Key]
        public int ConglomerateId { get; set; }
        public int StoreId { get; set; }
        public string ConglomerateName { get; set; }
        public bool Deleted { get; set; }
    }
}
