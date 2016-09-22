using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Checks
    {
        [Key]
        public int CheckId { get; set; }
        public int StoreId { get; set; }
        public int CheckNum { get; set; }
        public DateTime CheckDate { get; set; }
        public decimal CheckAmt { get; set; }
        public string CurrencyCode { get; set; }
        public string PayeeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Memo { get; set; }
        public bool Printed { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditBy { get; set; }
        public DateTime EditDate { get; set; }
        public bool? CheckRequest { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public string HoCode { get; set; }
    }
}
