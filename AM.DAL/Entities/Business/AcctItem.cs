using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class AcctItem
    {
        [Key, Column(Order = 0)]
        public int ResGrpId { get; set; }
        [Key, Column(Order = 1)]
        public bool Grp { get; set; }
        [Key, Column(Order = 2)]
        public short LineItemSeq { get; set; }
        public string CurrencyCode { get; set; }
        public int VendorId { get; set; }
        public string ProductTypeCode { get; set; }
        public int StoreId { get; set; }
        public decimal StoreAmt { get; set; }
        public decimal VendorAmt { get; set; }
        public decimal FeesTaxesAmt { get; set; }
        public decimal CommAmt { get; set; }
        public decimal OtherCommAmt { get; set; }
        public decimal ProfitAmt { get; set; }
        public decimal RoyaltyPrct { get; set; }
        public decimal RoyaltyAmt { get; set; }
        public decimal SubjToRoyaltyAmt { get; set; }
        public decimal StorePriceAmt { get; set; }
        public decimal VendorCostAmt { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal UserCommAmt { get; set; }
        public decimal UserCommPaidAmt { get; set; }
        public decimal UserCommBalAmt { get; set; }
        public decimal? UserCommPrct { get; set; }
        public decimal RevenuePaidAmt { get; set; }
        public decimal RevenueBalAmt { get; set; }
        public DateTime? RevenuePayableDate { get; set; }
        public decimal? GstPaidAmt { get; set; }
        public decimal? GstBalAmt { get; set; }
        public decimal? GstVendorAmt { get; set; }
        public decimal? GstCommAmt { get; set; }
        public decimal? GstCdCommAmt { get; set; }
        public decimal? GstCdPaidAmt { get; set; }
        public decimal? GstCdBalAmt { get; set; }
        public decimal? GstRoyaltyAmt { get; set; }
    }
}
