using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.Entities.Business
{
    public class InterestCat : AuditableEntity
    {
        [Key]
        public int InterestCatId { get; set; }
        public string InterestCatName { get; set; }
    }
}
