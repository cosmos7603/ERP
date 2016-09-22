using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GiftType : AuditableEntity
    {
        [Key]
        public int GiftTypeId { get; set; }
        public int StoreId { get; set; }
        public string GiftTypeName { get; set; }
        public int VendorId { get; set; }
        public decimal? VendorAmt { get; set; }
        public string GiftUnitCode { get; set; }
        public bool Deleted { get; set; }
    }
}
