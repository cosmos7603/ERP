using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PromotionDestination
    {
        [Key]
        public int PromotionDestinationId { get; set; }
        public int PromotionId { get; set; }
        public int DestinationId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
