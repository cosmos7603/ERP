using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{

    public class ReservationGetVendorInfoResult
    {
        public string VendorName { get; set; }
        public string ProductTypeName { get; set; }
        public string ConfirmationNumber { get; set; }
        public string GrpConfirmationNumber { get; set; }
        public string IataPhoneNumber { get; set; }
        public string ItemDescr { get; set; }
        public bool PrimaryActivity { get; set; }
        public string IataCode { get; set; }
        public string IataStoreName { get; set; }
    }
}
