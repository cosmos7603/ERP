using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PromotionBrand
    {
        [Key, Column(Order = 0)]
        public int PromotionId { get; set; }
        [Key, Column(Order = 1)]
        public string BrandCode { get; set; }
    }
}
