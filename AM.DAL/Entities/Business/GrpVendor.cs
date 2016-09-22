using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpVendor
    {
        [Key, Column(Order = 0)]
        public int GrpId { get; set; }
        [Key, Column(Order = 1)]
        public int VendorId { get; set; }
        [Key, Column(Order = 2)]
        public string CurrencyCode { get; set; }
        public decimal? ProtCommAmt { get; set; }
        public decimal? VendorChargeAmt { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
