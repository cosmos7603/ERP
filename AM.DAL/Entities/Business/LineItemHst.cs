using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LineItemHst
    {
        [Key]
        public int LiHstId { get; set; }
        public int ReservationId { get; set; }
        public short LineItemSeq { get; set; }
        public string ProductTypeCode { get; set; }
        public int? VendorId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? VendorAmt { get; set; }
        public decimal? StoreAmt { get; set; }
        public decimal? CommPrct { get; set; }
        public decimal? TaxFeeAmt { get; set; }
        public decimal? GovFeeAmt { get; set; }
        public decimal? OtherCommAmt { get; set; }
        public string HstEventType { get; set; }
        public DateTime HstCreateDate { get; set; }
        public string HstCreateBy { get; set; }
        public decimal? MarkupDsctAmt { get; set; }
        public string ItemName { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string HotelAccommodationType { get; set; }
    }
}
