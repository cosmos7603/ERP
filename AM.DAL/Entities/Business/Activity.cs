using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }
        public int? AutomaticRuleId { get; set; }
        public int StoreId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityDetails { get; set; }
        public decimal ActivityAmt { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
