using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class MktgActivity : AuditableEntity
    {
        [Key]
        public int MktgActivityId { get; set; }
        public string MktgActivityName { get; set; }
        public DateTime DropDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int? DataFileId { get; set; }
        public string SilverpopMailingCode { get; set; }
    }
}
