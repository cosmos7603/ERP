using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LeadRequest
    {
        [Key]
        public int LeadRequestId { get; set; }
        public string MemberId { get; set; }
        public string RequestType { get; set; }
        public int? RequestId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string CounselorLogin { get; set; }
        public int? DestinationId { get; set; }
        public int? CruiseLineId { get; set; }
        public int? ShipId { get; set; }
        public int? MktgSrcId { get; set; }
        public DateTime? TravelDate { get; set; }
        public int? Duration { get; set; }
        public string PackageName { get; set; }
        public string PackageLink { get; set; }
        public string CabinTypes { get; set; }
        public bool IncludeAirfare { get; set; }
        public string Airport { get; set; }
        public bool IncludeInsurance { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public bool ReceiveNewsletter { get; set; }
        public string Comments { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public bool Mobile { get; set; }
        public string MobileManufacturer { get; set; }
        public string MobileDeviceModel { get; set; }
        public string IpAddress { get; set; }
        public string InitialReferrer { get; set; }
        public string Url { get; set; }
        public string PreviousPageReferrer { get; set; }
        public string PreviousPageUtmMedium { get; set; }
        public string PreviousPageUtmSource { get; set; }
        public string PreviousPageUtmCampaign { get; set; }
        public string PlanningProcess { get; set; }
        public string LastCruise { get; set; }
        public string LastCruiseDuration { get; set; }
        public string LastCruiseLine { get; set; }
        public bool Pending { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
