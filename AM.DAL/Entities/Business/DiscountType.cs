using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class DiscountType : AuditableEntity
    {
        [Key]
        public int DiscountTypeId { get; set; }
        public string DiscountTypeName { get; set; }
        public int StoreId { get; set; }
        public bool Deleted { get; set; }
    }
}
