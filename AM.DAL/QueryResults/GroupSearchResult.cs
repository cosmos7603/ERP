using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class GroupSearchResult
    {
        public int GrpId { get; set; }
        public string GrpName { get; set; }
        public string GrpTypeCode { get; set; }
        public string GrpTypeName { get; set; }
        public string StatusCode { get; set; }
        public DateTime? PriActBeginDate { get; set; }
        public string ShipName { get; set; }
        public Int16? ShipId { get; set; }
        public string ConfirmationNumber { get; set; }
        public string VendorAgentName { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public string ItineraryName { get; set; }
        public Int16? DurationNum { get; set; }
        public string DestinationName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string BrandCode { get; set; }
        public string CounselorLogin { get; set; }
        public string CounselorName { get; set; }
        public bool Reconciled { get; set; }
        public int ResCount { get; set; }
    }
}
