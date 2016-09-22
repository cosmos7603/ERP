using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class ReservationGetPricingSummaryResult
    {
        public int SeqNum { get; set; }
	    public int? PrimaryActivity { get; set; }
        public string VendorName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? StorePriceAmt { get; set; }
        public decimal? VendorPriceAmt { get; set; }
        public decimal? CommAmt { get; set; }
        public decimal? ProfitAmt { get; set; }
        public decimal? FeesTaxesAmt { get; set; }
        public decimal? CpaidAmt { get; set; }
        public decimal? CpaidWithPendAmt { get; set; }
        public decimal? CbalAmt { get; set; }
        public decimal? CbalWithPendAmt { get; set; }
        public decimal? VpaidAmt { get; set; }
        public decimal? VbalAmt { get; set; }
        public decimal? YieldPrct { get; set; }
        public decimal? GstCommAmt { get; set; }
    }
}
