using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class CustomerLegacyRes
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string LegacyBookingCode { get; set; }
        public string VendorName { get; set; }
        public int? VendorId { get; set; }
        public string ActivityDetails { get; set; }
        public DateTime? SailDate { get; set; }
        public decimal TotalAmt { get; set; }
    }
}
