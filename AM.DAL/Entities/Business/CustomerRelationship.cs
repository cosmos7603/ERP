using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerRelationship
    {
        [Key]
        public int CustomerRelationshipId { get; set; }
        public int CustomerId1 { get; set; }
        public int CustomerId2 { get; set; }
        public int RelationshipId { get; set; }
    }
}
