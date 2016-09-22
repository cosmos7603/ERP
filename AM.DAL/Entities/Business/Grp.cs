using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Grp : AuditableEntity
    {
        [Key]
        public int GrpId { get; set; }
        public int StoreId { get; set; }
        public string GrpName { get; set; }
        public string GrpTypeCode { get; set; }
        public string StatusCode { get; set; }
        public DateTime? PriActBeginDate { get; set; }
        public DateTime? PriActEndDate { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? PaxDepartDate { get; set; }
        public DateTime? PaxReturnDate { get; set; }
        public bool Incentive { get; set; }
        public string CounselorLogin { get; set; }
        public int? ContactCustId { get; set; }
        public DateTime? CustDpst1DueDate { get; set; }
        public decimal? CustDpst1Amt { get; set; }
        public DateTime? CustDpst2DueDate { get; set; }
        public decimal? CustDpst2Amt { get; set; }
        public DateTime? CustDpst3DueDate { get; set; }
        public decimal? CustDpst3Amt { get; set; }
        public DateTime? CustFnlPymtDate { get; set; }
        public int? InvCustomerId { get; set; }
        public string InvoiceInfo { get; set; }
        public string ResInvoiceInfo { get; set; }
        public bool Deleted { get; set; }
        public string RoyaltyFlatCurr { get; set; }
        public decimal? RoyaltyFlatAmt { get; set; }
        public bool? Inv2ndPage { get; set; }
        public bool? InvPreprinted { get; set; }
        public bool? InvIncludePortTax { get; set; }
        public bool? InvDisclaimer { get; set; }
        public bool? InvPrintSavings { get; set; }
        public byte TcRatio { get; set; }
        public string ShareCommDetails { get; set; }
        public string Amenities { get; set; }
        public bool PreventBookings { get; set; }
        public bool HidePricings { get; set; }
        public int? AttachmentFileId { get; set; }
        public bool UseNameOnResInv { get; set; }
        public string IncentivePricingTypeCode { get; set; }
        public bool? ManageAssociate { get; set; }
        public bool? SharedStoreAssociates { get; set; }
        public string LegacyGroupCode { get; set; }
        public bool Closed { get; set; }
        public bool PreventAgencyPymtPriorRcon { get; set; }
        public bool Reconciled { get; set; }
        public bool EnableGrpSegments { get; set; }
        public bool Publish { get; set; }
        public string PublishDetails { get; set; }
        public string PublishPaymentBox { get; set; }
        public string PublishAmenities { get; set; }
        public string PublishCruiseDesc { get; set; }
        public bool ApiPublish { get; set; }
        public bool ApiIncludeSearch { get; set; }
        public string ApiCalloutName { get; set; }
        public string ApiHighlightedOffer { get; set; }
        public string ApiImageUrl1 { get; set; }
        public string ApiImageUrl2 { get; set; }
        public int? ApiFileId { get; set; }
        public string ApiSeoTitle { get; set; }
        public string ApiSeoDescription { get; set; }
        public string ApiSeoKeywords { get; set; }
        public string PublishDisclaimer { get; set; }
        public bool PublishIncludeAddonPricings { get; set; }
        public bool PublishAllowMultiplePricings { get; set; }
        public bool ApiFeature { get; set; }
        public bool IncludeAlternativePricingsSearch { get; set; }
        public string ApiOfferTitle { get; set; }
        public string ApiImageUrl3 { get; set; }
        public bool ApiAlwaysPublishLaf { get; set; }
        public string ConsumerUrlId { get; set; }
        public bool Locked { get; set; }
        public decimal? LafOverrideAmt { get; set; }
        public DateTime? ValidBeginDate { get; set; }
        public DateTime? ValidEndDate { get; set; }
    }
}
