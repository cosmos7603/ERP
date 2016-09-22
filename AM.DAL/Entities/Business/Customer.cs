using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Customer : AuditableEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string OrganizationName { get; set; }
        public string PriAddress1 { get; set; }
        public string PriAddress2 { get; set; }
        public string PriCity { get; set; }
        public string PriStateName { get; set; }
        public string PriZip { get; set; }
        public string PriCountryCode { get; set; }
        public string SecAddress1 { get; set; }
        public string SecAddress2 { get; set; }
        public string SecCity { get; set; }
        public string SecStateName { get; set; }
        public string SecZip { get; set; }
        public string SecCountryCode { get; set; }
        public string Email { get; set; }
        public DateTime? EmailOptInDate { get; set; }
        public string CitizenCountryCode { get; set; }
        public string PassportNum { get; set; }
        public DateTime? PassportExpDate { get; set; }
        public short? PreTitleCode { get; set; }
        public short? PostTitleCode { get; set; }
        public string EnvelopeSalutation { get; set; }
        public bool RewardCruiseClub { get; set; }
        public string RewardCruiseClubNum { get; set; }
        public short? NumSailedOtherStore { get; set; }
        public int? MktgSrcId { get; set; }
        public decimal? PrefBudgetFromAmt { get; set; }
        public decimal? PrefBudgetToAmt { get; set; }
        public short? PrefDurationFrom { get; set; }
        public short? PrefDurationTo { get; set; }
        public string CounselorLogin { get; set; }
        public DateTime? LastContactDate { get; set; }
        public DateTime? BirthdayDate { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        public short? NumSailedCruises { get; set; }
        public decimal? TotalSailedAmt { get; set; }
        public short? NumNonCruises { get; set; }
        public decimal? TotalNonCruiseAmt { get; set; }
        public short? NumTripsLess7Days { get; set; }
        public short? NumTrips7Days { get; set; }
        public DateTime? AddedToListDate { get; set; }
        public DateTime? LastSailDate { get; set; }
        public string CustomerComments { get; set; }
        public bool Deleted { get; set; }
        public string FormLetterSalutation { get; set; }
        public int? ImportId { get; set; }
        public string SecEmail { get; set; }
        public string EmailType { get; set; }
        public string StatusCode { get; set; }
        public string NickName { get; set; }
        public int? EmailTypeCode { get; set; }
        public int? SecEmailTypeCode { get; set; }
        public int? PriAddrTypeCode { get; set; }
        public int? PriAddrFromMonth { get; set; }
        public int? PriAddrToMonth { get; set; }
        public int? SecAddrTypeCode { get; set; }
        public int? SecAddrFromMonth { get; set; }
        public int? SecAddrToMonth { get; set; }
        public int? MaritalStatusCode { get; set; }
        public string PriStateCode { get; set; }
        public string SecStateCode { get; set; }
        public string Gender { get; set; }
        public string EmailSalutation { get; set; }
        public byte? Age { get; set; }
        public int? PrefStateroomTypeCode { get; set; }
        public int? PrefDiningCode { get; set; }
        public int? PrefTableSizeCode { get; set; }
        public int? PrefDietCode { get; set; }
        public int? PrefAirlineId { get; set; }
        public int? PrefAirGatewayId { get; set; }
        public int? PrefSeatingCode { get; set; }
        public string PrefOther { get; set; }
        public bool? HouseholdHead { get; set; }
        public bool? LastMinuteTravel { get; set; }
        public string LegacyCustomerCode { get; set; }
        public string PriAddress3 { get; set; }
        public string SecAddress3 { get; set; }
        public int? EmergencyContactId { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public bool PersonalPrefHandicap { get; set; }
        public bool PersonalPrefSmoking { get; set; }
        public bool PersonalPrefAlcohol { get; set; }
        public int? PersonalPrefShirtSizeId { get; set; }
        public int? PassportIssuedById { get; set; }
        public DateTime? PassportIssuedDate { get; set; }
        public string PassportCountryCode { get; set; }
        public decimal? AvgSailedAmt { get; set; }
        public decimal? AvgNonCruiseAmt { get; set; }
        public decimal? LifetimeValueAmt { get; set; }
        public int? LastReservationId { get; set; }
        public int? NextReservationId { get; set; }
        public int? StoreAssociateAgentId { get; set; }
        public string CohortsCode { get; set; }
        public string CohortsEncodeFlag { get; set; }
        public string NcoaUpdateReason { get; set; }
        public int? BranchId { get; set; }
        public string GlobalEntryNumber { get; set; }
        public int? HouseholdIndex { get; set; }
        public string HouseholdKey { get; set; }

		public virtual List<CustomerPhone> CustomerPhones { get; set; }
	}
}
