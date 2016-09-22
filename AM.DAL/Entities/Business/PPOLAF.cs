using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PPOLAF
    {
        public int PpoLafId { get; set; }
        public int PpoOfferId { get; set; }
        public int PpoPriceId { get; set; }
        public int VendorId { get; set; }
        public int ShipId { get; set; }
        public DateTime SailDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int DurationNum { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime LafDate { get; set; }
        public decimal? LafAmt { get; set; }
        public decimal? InsideLafAmt { get; set; }
        public string InsideLafCatCode { get; set; }
        public decimal? OutsideLafAmt { get; set; }
        public string OutsideLafCatCode { get; set; }
        public decimal? BalconyLafAmt { get; set; }
        public string BalconyLafCatCode { get; set; }
        public decimal? SuiteLafAmt { get; set; }
        public string SuiteLafCatCode { get; set; }
        public bool NotLaf { get; set; }
        public bool Promotion { get; set; }
    }
}
