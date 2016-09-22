using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class DestinationArea : AuditableEntity
    {
        [Key]
        public short DestAreaId { get; set; }
        public string DestAreaName { get; set; }
        public bool Deleted { get; set; }
    }
}
