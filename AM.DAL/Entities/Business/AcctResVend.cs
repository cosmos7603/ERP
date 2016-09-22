using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AcctResVend
    {
        [Key, Column(Order = 0)]
        public int ResGrpId { get; set; }
        [Key, Column(Order = 1)]
        public bool Grp { get; set; }
        [Key, Column(Order = 2)]
        public string CurrencyCode { get; set; }
        [Key, Column(Order = 3)]
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
        public decimal? GstOwedGovAmt { get; set; }
        public decimal? GstOwedVendAmt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public decimal? RoyaltyAmt { get; set; }
        public decimal? StoreBalAmt { get; set; }
        public decimal? SubjToRoyaltyAmt { get; set; }
        public decimal CorporateChargeAmt { get; set; }
        public decimal? DepositAmt { get; set; }
        public decimal? RecvCorpAmt { get; set; }
        public decimal? DisbVendPendAmt { get; set; }
        public decimal? RecvVendPendAmt { get; set; }
        public decimal? StorePaidAmt { get; set; }
        public decimal? StorePayableAmt { get; set; }
        public DateTime? StorePayableDate { get; set; }
        public bool Centralized { get; set; }
        public decimal? RoyaltyPrct { get; set; }
        public DateTime? StorePaidDate { get; set; }
        public decimal? GstCommAmt { get; set; }
        public decimal? GstPaidAmt { get; set; }
        public decimal? GstBalAmt { get; set; }
        public decimal? GstPayableAmt { get; set; }
        public decimal? GstRoyaltyAmt { get; set; }
        public decimal? VendorPriceAmt { get; set; }
    }
}
