using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CruisePromotion
    {
        [Key, Column(Order = 0)]
        public int CruiseId { get; set; }
        [Key, Column(Order = 1)]
        public int PromotionId { get; set; }
        public bool? Publish { get; set; }
        public string LafCabinCatCode { get; set; }
        public bool LafOverride { get; set; }
        public decimal? LafOverrideAmt { get; set; }
    }
}
