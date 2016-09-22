using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LineItemAdj : AuditableEntity
    {
        [Key]
        public int LineItemAdjId { get; set; }
        public int ReservationId { get; set; }
        public int? StoreId { get; set; }
        public int? LineItemSeq { get; set; }
        public string ProductTypeCode { get; set; }
        public int? VendorId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? StoreAdjAmt { get; set; }
        public decimal? VendorAdjAmt { get; set; }
        public decimal? TaxAdjAmt { get; set; }
        public decimal? OtherCommAdjAmt { get; set; }
        public string AdjustTypeCode { get; set; }
        public decimal? MarkupAdjAmt { get; set; }
        public decimal? ProtCommAdjAmt { get; set; }
        public decimal? CommAdjAmt { get; set; }
        public decimal? ProfitAdjAmt { get; set; }
    }
}
