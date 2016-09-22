using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Preference : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int PassengerId { get; set; }
        [Key, Column(Order = 1)]
        public string CodeType { get; set; }
        [Key, Column(Order = 2)]
        public int CodeId { get; set; }
        public bool SailedAgency { get; set; }
        public bool SailedOther { get; set; }
        public bool Preferred { get; set; }
    }
}
