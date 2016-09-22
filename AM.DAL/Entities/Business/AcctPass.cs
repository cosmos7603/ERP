using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AcctPass
    {
        [Key, Column(Order = 0)]
        public int ResGrpId { get; set; }
        [Key, Column(Order = 1)]
        public bool Grp { get; set; }
        [Key, Column(Order = 2)]
        public int PassengerId { get; set; }
        [Key, Column(Order = 3)]
        public string CurrencyCode { get; set; }
        public int StoreId { get; set; }
        public decimal? ApCustAmt { get; set; }
        public decimal? ArCustAmt { get; set; }
        public decimal? StoreAmt { get; set; }
        public decimal? FeesTaxesAmt { get; set; }
        public decimal? ProfitAmt { get; set; }
        public decimal? RecvCustAmt { get; set; }
        public decimal? DisbCustAmt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public decimal? RecvCustPendChckAmt { get; set; }
        public decimal? RecvCustPendCcspAmt { get; set; }
        public decimal? RecvCustPendUccvAmt { get; set; }
        public decimal? RecvCustPendAmt { get; set; }
        public decimal? DisbCustPendAmt { get; set; }
        public decimal? StorePriceAmt { get; set; }
    }
}
