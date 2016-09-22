using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    [Table("Users")]
    public partial class User : AuditableEntity
	{
		[Key]
		public string Login { get; set; }
        public int StoreId { get; set; }
		public bool OwnData { get; set; }
		public bool DisableCounselor { get; set; }
		public bool CounselorDefaults { get; set; }
		public bool CounselorInv { get; set; }
		public bool ReopenRes { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string CountryCode { get; set; }
		public string StateName { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }
		public string TollFreePhone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public string Website { get; set; }
		public string CurrencyCode { get; set; }
		public bool Deleted { get; set; }
		public bool Active { get; set; }
		public string OracleSupplierSite { get; set; }
		public string UserLevelCode { get; set; }
		public string CorpBrandCode { get; set; }
		public string CorpHoCode { get; set; }
		public string CounselorTitle { get; set; }
		public string CounselorCredentials1 { get; set; }
		public string CounselorCredentials2 { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullNameBak { get; set; }
		public int? BranchId { get; set; }
		public string CounselorCredentialsOther1 { get; set; }
		public string CounselorCredentialsOther2 { get; set; }
		public bool CantChangeResDate { get; set; }
		public bool? DefOnlineBookings { get; set; }
		public bool? ChangeOnlineBookings { get; set; }
		public string Password { get; set; }
		public bool ChangePassword { get; set; }
		public bool OwnReportData { get; set; }
		public string WtpLogin { get; set; }
		public string WtpPassword { get; set; }
		public bool WtpOpen { get; set; }
		public bool? CanEditHelp { get; set; }
		public string PhoneExtension { get; set; }
		public bool InvGuestBreakdown { get; set; }
		public bool InvCruiseItinerary { get; set; }
		public bool InvAirItinerary { get; set; }
		public bool InvWindowEnvelope { get; set; }
		public bool InvSuppressHeader { get; set; }
		public bool InvInsuranceDisclaimer { get; set; }
		public string SabrePcc { get; set; }
		public string HeaderCode { get; set; }
		public bool InvAltHeaders { get; set; }
		public int? EnactStoreId { get; set; }
		public string EnactBrandCode { get; set; }
		public string SabreUsername { get; set; }
		public string SabrePassword { get; set; }
		public string ShoreExcGroupId { get; set; }
		public bool ViewUserComm { get; set; }
		public bool ChangeUserComm { get; set; }
		public string UserPymtRuleCode { get; set; }
		public bool CantChangeBranch { get; set; }
		public bool InvShowMiddleName { get; set; }
		public bool InvShowDob { get; set; }
		public string LegacyUserCode { get; set; }
		public bool NonAccess { get; set; }
		public string AccountManagerLogin { get; set; }
		public string AccountManagerDesc { get; set; }
		public bool EditLockedTransactions { get; set; }
		public bool InvShowCountry { get; set; }
		public string AbsorbLogin { get; set; }
		public string AbsorbPassword { get; set; }
		public string RelatedLogin { get; set; }
		public bool NonvoyageTransfer { get; set; }
		public bool InvGuestDates { get; set; }
		public bool InvInsuranceDates { get; set; }
		public bool RestrictedGrpAccess { get; set; }
		public bool InvTravelingWith { get; set; }
		public string VirtuosoAgentCode { get; set; }
		public bool CanAddCePrinted { get; set; }
		public bool ReminderFilterFinancial { get; set; }
		public bool ReminderFilterCe { get; set; }
		public bool ReminderFilterResGrp { get; set; }
		public bool ReminderFilterCustomerLead { get; set; }
		public bool ReminderFilterDiscretionary { get; set; }
		public string ReminderFilterDateRange { get; set; }
		public bool ReminderFilterSuppressOverdue { get; set; }
		public string ReminderFilterLogin { get; set; }
		public bool LegalBankingInfo { get; set; }
		public bool InvPayNow { get; set; }
		public bool FullBankingRegister { get; set; }
		public bool CanEditAuSyncid { get; set; }
		public bool InterfaceAu { get; set; }
		public string AuSyncid { get; set; }
		public bool AuOpen { get; set; }
		public bool EnableFtbHtml { get; set; }
		public bool PreventEditNotes { get; set; }
		public string SessionId { get; set; }
		public int? LockReservationId { get; set; }
		public int? LockGrpId { get; set; }
		public bool EngagedIc { get; set; }
		public string TanId { get; set; }
		public string PublicHeaderUrl { get; set; }
		public string PublicFooterUrl { get; set; }
		public string PublicHeaderGrpRegUrl { get; set; }
		public string PublicFooterGrpRegUrl { get; set; }
		public bool CanStealLocked { get; set; }
		public bool MktgOptInEmail { get; set; }
		public bool MktgOptInPrinted { get; set; }
		public string MktgOptInPrintType { get; set; }
		public int? EngagementLogoId { get; set; }
		public string SessionTimestamp { get; set; }
		public bool CanEditAtRisk { get; set; }
		public string LeadFilterStatus { get; set; }
		public string LeadFilterClient { get; set; }
		public int? LeadFilterBranch { get; set; }
		public string LeadFilterCounselor { get; set; }
		public string LeadFilterVendor { get; set; }
		public string LeadFilterShip { get; set; }
		public string LeadFilterDestination { get; set; }
		public DateTime? LeadFilterOriginBeginDate { get; set; }
		public DateTime? LeadFilterOriginEndDate { get; set; }
		public DateTime? LeadFilterNextFollowupBeginDate { get; set; }
		public DateTime? LeadFilterNextFollowupEndDate { get; set; }
		public DateTime? LeadFilterLastContactBeginDate { get; set; }
		public DateTime? LeadFilterLastContactEndDate { get; set; }
		public string LeadFilterFollowupTime { get; set; }
		public bool? LeadFilterIncludeUnassigned { get; set; }
		public bool CanEnterStoreNews { get; set; }
		public bool AllowNegativeStorePayments { get; set; }
		public bool HelpOpen { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public string FullName { get; set; }

		[ForeignKey("StoreId")]
		public virtual Store Store { get; set; }

		[ForeignKey("CorpBrandCode")]
		public virtual Brand CorpBrand { get; set; }

		[ForeignKey("CorpHoCode")]
		public virtual HeadOffice CorpHeadOffice { get; set; }
	}
}
