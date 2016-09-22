using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AM.DAL.Entities.Business;

namespace AM.DAL
{
    public class LineItem : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public int ReservationId { get; set; }
        [Key, Column(Order = 1)]
        public short LineItemSeq { get; set; }
        public string ProductTypeCode { get; set; }
        public int VendorId { get; set; }
        public string ItemDescr { get; set; }
        public string ItemName { get; set; }
        public string CurrencyCode { get; set; }
        public string ConfirmationNumber { get; set; }
        public int? CruiseLineId { get; set; }
        public short? ShipId { get; set; }
        public int? ItineraryId { get; set; }
        public string CabinSizeCode { get; set; }
        public string CabinCatCode { get; set; }
        public string CabinNumber { get; set; }
        public string VendorAgentName { get; set; }
        public bool? PrimaryActivity { get; set; }
        public bool AirIncluded { get; set; }
        public bool SuppressLine { get; set; }
        public int? GrpId { get; set; }
        public short? GrpLineItemSeq { get; set; }
        public DateTime VndrFnlPymtDate { get; set; }
        public bool LiAdded { get; set; }
        public string LiStatusCode { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? BusStopId { get; set; }
        public string ItemComments { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public bool? FitItem { get; set; }
        public int? TourPropertyId { get; set; }
        public string HotelAccommodationType { get; set; }
        public string FareCode { get; set; }
        public int? GrpLinePricingId { get; set; }
        public bool SingleOccupancy { get; set; }
        public int? CarTypeId { get; set; }
        public DateTime? VndrDpstDate { get; set; }
        public string PaidByCode { get; set; }
        public bool? CruiseTour { get; set; }
        public bool Centralized { get; set; }
        public bool NotIncluded { get; set; }
        public string HotelContactInfo { get; set; }
        public int? GiftAppointmentId { get; set; }
        public string LegacyItemCode { get; set; }
        public int? GiftTypeId { get; set; }
        public string GiftUnitCode { get; set; }
        public string GiftStatusCode { get; set; }
        public DateTime? GiftOrderDate { get; set; }
        public DateTime? GiftProcessedDate { get; set; }
        public int? DiscountTypeId { get; set; }
        public int? IataId { get; set; }
        public string CabinCatCodePurchased { get; set; }
        public int? OtherTypeId { get; set; }
        public string InsuranceEngineStatus { get; set; }
        public bool FareCodeConfirmed { get; set; }

		public virtual Ship Ship { get; set; }
		public virtual ProductType ProductType { get; set; }
		public virtual Itinerary Itinerary { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual GrpLineItem GrpLineItem { get; set; }
        public virtual IATA Iata { get; set; }
	}
}
