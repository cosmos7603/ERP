using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LinePass : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int ReservationId { get; set; }
        [Key, Column(Order = 1)]
        public short LineItemSeq { get; set; }
        [Key, Column(Order = 2)]
        public int PassengerId { get; set; }
        public decimal? BrochureAmt { get; set; }
        public decimal VendorAmt { get; set; }
        public decimal StoreAmt { get; set; }
        public decimal? CommPrct { get; set; }
        public decimal? TaxFeeAmt { get; set; }
        public decimal? GovFeeAmt { get; set; }
        public decimal? OtherCommAmt { get; set; }
        public string GatewayCity { get; set; }
        public decimal? RoyaltyPrct { get; set; }
        public decimal? DepositAmt { get; set; }
        public decimal? GstVendorAmt { get; set; }
        public decimal? GstCommAmt { get; set; }
        public decimal? MarkupDsctAmt { get; set; }
        public decimal? CommAmt { get; set; }
        public decimal? VendorCostAmt { get; set; }
        public decimal? ProfitAmt { get; set; }
    }
}
