using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PromotionPublish
    {
        [Key]
        public int PromotionId { get; set; }
        public int CruiseId { get; set; }
        public decimal Price1Amt { get; set; }
        public decimal Price2Amt { get; set; }
        public decimal Price3Amt { get; set; }
        public decimal Price4Amt { get; set; }
        public string Price1Title { get; set; }
        public string Price2Title { get; set; }
        public string Price3Title { get; set; }
        public string Price4Title { get; set; }
        public string LargerPrint { get; set; }
        public string SmallerPrint { get; set; }
        public string SmallBottomPrint { get; set; }
    }
}
