using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AcctPassVend
    {
        [Key, Column(Order = 0)]
        public int ResGrpId { get; set; }
        [Key, Column(Order = 1)]
        public bool Grp { get; set; }
        [Key, Column(Order = 2)]
        public int PassengerId { get; set; }
        [Key, Column(Order = 3)]
        public string CurrencyCode { get; set; }
        [Key, Column(Order = 4)]
        public int VendorId { get; set; }
        public int StoreId { get; set; }
        public decimal? ApVendAmt { get; set; }
        public decimal? ArVendAmt { get; set; }
        public decimal? FeesTaxesAmt { get; set; }
        public decimal? StoreAmt { get; set; }
        public decimal? VendorAmt { get; set; }
        public decimal? CommAmt { get; set; }
        public decimal? OtherCommAmt { get; set; }
        public decimal? ProfitAmt { get; set; }
        public decimal? DisbVendAmt { get; set; }
        public decimal? RecvVendAmt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public decimal? RecvCorpAmt { get; set; }
        public decimal? VendorPriceAmt { get; set; }
    }
}
