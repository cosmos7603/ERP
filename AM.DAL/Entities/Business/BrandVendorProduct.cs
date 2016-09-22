using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BrandVendorProduct : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int VendorId { get; set; }
        [Key, Column(Order = 1)]
        public string ProductTypeCode { get; set; }
        [Key, Column(Order = 2)]
        public string BrandCode { get; set; }
        public decimal? CommPrct { get; set; }
        public decimal? GroupCommPrct { get; set; }
    }
}
