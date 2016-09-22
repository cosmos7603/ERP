using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AccountAdj
    {
        [Key]
        public int AccountAdjId { get; set; }
        public string AccountCode { get; set; }
        public int StoreId { get; set; }
        public decimal AdjAmt { get; set; }
        public string CurrencyCode { get; set; }
        public string CheckNum { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CheckId { get; set; }
        public int? ReconciliationSetupId { get; set; }
    }
}
