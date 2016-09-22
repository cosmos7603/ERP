using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class GrpLineItem
    {
        [Key, Column(Order = 0)]
        public int GrpId { get; set; }
        [Key, Column(Order = 1)]
        public short GrpLineItemSeq { get; set; }
        public string ProductTypeCode { get; set; }
        public int VendorId { get; set; }
        public string ItemDescr { get; set; }
        public string ItemName { get; set; }
        public string CurrencyCode { get; set; }
        public string ConfirmationNumber { get; set; }
        public string VendorAgentName { get; set; }
        public int? CruiseLineId { get; set; }
        public short? ShipId { get; set; }
        public int? ItineraryId { get; set; }
        public string GatewayCity { get; set; }
        public bool PrimaryActivity { get; set; }
        public bool AirIncluded { get; set; }
        public DateTime? VndrDpst1DueDate { get; set; }
        public decimal? VndrDpst1Amt { get; set; }
        public DateTime? VndrDpst2DueDate { get; set; }
        public decimal? VndrDpst2Amt { get; set; }
        public DateTime? VndrDpst3DueDate { get; set; }
        public decimal? VndrDpst3Amt { get; set; }
        public DateTime? VndrFnlPymtDate { get; set; }
        public string PaidByCode { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }
        public bool ManageInventory { get; set; }
        public int? BusStopId { get; set; }
        public string ItemComments { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public bool? PackageItem { get; set; }
        public decimal? UserCommPrct { get; set; }
        public string HotelAccommodationType { get; set; }
        public decimal? RoyaltyPrct { get; set; }
        public string InventoryUnitCode { get; set; }
        public int? TourPropertyId { get; set; }
        public bool LockPricings { get; set; }
        public int? CarTypeId { get; set; }
        public bool? CruiseTour { get; set; }
        public bool PreventRemoval { get; set; }
        public bool InvBundleItem { get; set; }
        public bool ResPaidByControl { get; set; }
        public bool InvPrimaryItem { get; set; }
        public bool InvSuppressItem { get; set; }
        public bool? Centralized { get; set; }
        public string HotelContactInfo { get; set; }
        public bool LockPd2Pricings { get; set; }
        public bool? UserCommOverride { get; set; }
        public decimal? UserCommAmt { get; set; }
        public short? OrderSeq { get; set; }
        public string UserCommTypeCode { get; set; }
        public int? GiftTypeId { get; set; }
        public string GiftUnitCode { get; set; }
        public DateTime? GiftOrderDate { get; set; }
        public DateTime? GiftProcessedDate { get; set; }
        public string GiftStatusCode { get; set; }
        public int? GiftAppointmentId { get; set; }
        public string GiftLevelCode { get; set; }
        public int? DiscountTypeId { get; set; }
        public int? IataId { get; set; }
        public int? OtherTypeId { get; set; }
    }
}
