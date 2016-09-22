using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class MktgActivityPromotion
    {
        [Key, Column(Order = 0)]
        public int Mktgactivityid { get; set; }
        [Key, Column(Order = 1)]
        public int Promotionid { get; set; }
		public Promotion Promotion { get; set; }
    }
}
