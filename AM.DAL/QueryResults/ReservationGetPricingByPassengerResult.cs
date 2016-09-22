using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class ReservationGetPricingByPassengerResult
    {
        public string ItemName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Pass01 { get; set; }
        public decimal? Pass02 { get; set; }
        public decimal? Pass03 { get; set; }
        public decimal? Pass04 { get; set; }
        public decimal? Pass05 { get; set; }
        public decimal? Pass06 { get; set; }
        public decimal? Pass07 { get; set; }
        public decimal? Pass08 { get; set; }
        public decimal? Total { get; set; }
    }
}
