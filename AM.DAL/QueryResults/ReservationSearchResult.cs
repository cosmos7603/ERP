using System;

namespace AM.DAL.QueryResults
{
    public class ReservationSearchResult
    {
        public int ReservationId { get; set; }
        public int StoreId { get; set; }
        public string StatusCode { get; set; }
        public int LeadPassengerId { get; set; }
        public int? GrpId { get; set; }
        public string CounselorLogin { get; set; }
        public DateTime? CustFnlPymtDate { get; set; }
        public DateTime? CustDpstDate { get; set; }
        public decimal? CustDpstAmt { get; set; }
        public string CustDpstCurr { get; set; }
        public DateTime? VndrPriActDpstDate { get; set; }
        public decimal? StoreChargeAmt { get; set; }
        public int? InvCustomerId { get; set; }
        public bool PendingInvoice { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime PriActBeginDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? QuotedDate { get; set; }
        public DateTime? PriActEndDate { get; set; }
        public int? CopyReservationId { get; set; }
        public int MktgSrcId { get; set; }
        public string RoyaltyFlatCurr { get; set; }
        public decimal? RoyaltyFlatAmt { get; set; }
        public bool FinanciallyClosed { get; set; }
        public bool? BookingApplyAll { get; set; }
        public bool? BookingSystemDates { get; set; }
        public int? BranchId { get; set; }
        public decimal? ResaleCommAmt { get; set; }
        public int? GrpLeaderId { get; set; }
        public DateTime? EmailFnlPymtDate { get; set; }
        public bool InvLineItemDet { get; set; }
        public bool? OnlineBooking { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public string InvEnvelopeSalutation { get; set; }
        public bool ExcludeFromReports { get; set; }
        public bool RetrievedFromSabre { get; set; }
        public bool Sga { get; set; }
        public string LegacyReservationCode { get; set; }
        public bool Closed { get; set; }
        public bool InquiryBooking { get; set; }
        public string BookingMethodCode { get; set; }
        public DateTime? CustPaidDate { get; set; }
        public int? LinkReservationId { get; set; }
        public string SabrePcc { get; set; }
        public DateTime? PrintFnlPymtDate { get; set; }
        public int? BerthOptionId { get; set; }
        public DateTime? ResSavedDate { get; set; }
        public bool ExpiredQuote { get; set; }
        public DateTime? ExpiredQuoteDate { get; set; }
        public bool RetrievedFromCruisepro { get; set; }
        public string AuSyncid { get; set; }
        public bool Locked { get; set; }
        public DateTime? SystemCancelDate { get; set; }
        public string CruiseproBookingOrigin { get; set; }
        public bool VirtuosoGroup { get; set; }
        public int? CancellationReasonId { get; set; }
        public int? FitLinkGrpId { get; set; }
        public bool WaitlistedQuote { get; set; }

        public string BrandCode { get; set; }
        public int Grp { get; set; }
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CabinCatCode { get; set; }
        public string CabinSizeCode { get; set; }
        public string CabinSizeName { get; set; }
        public string FullName { get; set; }
        public string ItemFullDescription { get; set; }
        public int? PaxCount { get; set; }
        public string StatusName { get; set; }
        public string ProductTypeName { get; set; }
    }
}
