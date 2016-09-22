using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class ReservationGetUserCommSummaryResult
    {
        public int OrderSeq { get; set; }
	    public int? LineItemSeq { get; set; }
        public string ItemTitle { get; set; }
        public string UserName { get; set; }
        public decimal? StoreAmt { get; set; }
        public decimal? ProfitAmt { get; set; }
        public string UserCommTypeCode { get; set; }
        public decimal? UserCommPrct { get; set; }
        public decimal? UserCommAmt { get; set; }
        public decimal? UserCommPaidAmt { get; set; }
        public decimal? UserCommBalAmt { get; set; }
    }
}
