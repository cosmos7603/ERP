using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Brand : AuditableEntity
    {
        [Key]
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string HoCode { get; set; }
        public string AppTitle { get; set; }
        public bool? CounselorInv { get; set; }
        public bool? ExcludePreprinted { get; set; }
        public bool? InvoiceDisclaimer { get; set; }
        public bool FranchiseePymtsBooked { get; set; }
        public string RoyaltyTypeCode { get; set; }
        public bool ConfNumRequired { get; set; }
        public bool RenewalFees { get; set; }
        public bool DocumentHandling { get; set; }
        public bool SendMails { get; set; }
        public bool ManageActivation { get; set; }
        public string SmtpFromAddress { get; set; }
        public bool? FranchiseePymtsNoCbal { get; set; }
        public bool? DisableCashPayments { get; set; }
        public string CustomerSalutationPrefix { get; set; }
        public bool? EmailFinalPayment { get; set; }
        public bool? DisableNewVendors { get; set; }
        public bool ManageGrpAgentComm { get; set; }
        public bool PendingDeposit { get; set; }
        public bool ExpediteTransaction { get; set; }
        public bool? ConfNumGrpCopy { get; set; }
        public bool? RemindersUsePaxDepartDate { get; set; }
        public bool InvEntireTripDates { get; set; }
        public bool? OnlineBookings { get; set; }
        public string BrandShortName { get; set; }
        public bool InterfaceWtp { get; set; }
        public bool AdminStoreAccounting { get; set; }
        public bool AdminStoreGeneralList { get; set; }
        public string IntranetName { get; set; }
        public bool InterfaceSabre { get; set; }
        public bool Active { get; set; }
        public bool EnableSga { get; set; }
        public bool InvoiceCurrencyLabel { get; set; }
        public bool EnableCentralized { get; set; }
        public bool ForceCentralized { get; set; }
        public bool? OutsideAgencyBookings { get; set; }
        public bool? DisableOldResWizard { get; set; }
        public bool ManageHqTrans { get; set; }
        public bool? CheckRequest { get; set; }
        public bool ManualChecks { get; set; }
        public bool CustomerAddress3 { get; set; }
        public bool DisableInvWindowEnvelope { get; set; }
        public bool AirRoyaltyExempt { get; set; }
        public bool PrepopulateConfNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string SecCurrencyCode { get; set; }
        public string AbsorbUrl { get; set; }
        public bool EnableVisaStatus { get; set; }
        public string DefSearchInventoryOptions { get; set; }
        public string CountryCode { get; set; }
        public bool Berth { get; set; }
        public bool? RoyaltyOnCanceled { get; set; }
        public bool EnableIata { get; set; }
        public bool DisableGrpBkgOtherCat { get; set; }
        public bool DefaultCcsp { get; set; }
        public bool WtpDashboard { get; set; }
        public string WtpUrl { get; set; }
        public bool? ShowLaf { get; set; }
        public bool InterfaceAu { get; set; }
        public bool EnableCraGst { get; set; }
        public bool EnableCePrinted { get; set; }

		[ForeignKey("HoCode")]
		public virtual HeadOffice HeadOffice { get; set; }

	}
}
