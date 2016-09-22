using System;

namespace AM.DAL.QueryResults
{
	public class CustomerGetListResult
	{
		public string FullName { get; set; }
		public int CustomerId { get; set; }
		public int StoreId { get; set; }
		public string OrganizationName { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleInit { get; set; }
		public string PriAddress1 { get; set; }
		public string PriAddress2 { get; set; }
		public string PriCity { get; set; }
		public string PriStateName { get; set; }
		public string PriZip { get; set; }
		public string PriCountryCode { get; set; }
		public string PriCountryName { get; set; }
		public string SecAddress1 { get; set; }
		public string SecAddress2 { get; set; }
		public string SecCity { get; set; }
		public string SecStateName { get; set; }
		public string SecZip { get; set; }
		public string SecCountryCode { get; set; }
		public string SecCountryName { get; set; }
		public string Email { get; set; }
		public DateTime? EmailOptInDate { get; set; }
		public string CitizenCountryCode { get; set; }
		public string PassportNum { get; set; }
		public DateTime? PassportExpDate { get; set; }
		public string PreTitle { get; set; }
		public string PostTitle { get; set; }
		public string EnvelopeSalutation { get; set; }
		public string FormLetterSalutation { get; set; }
		public bool? RewardCruiseClub { get; set; }
		public string RewardCruiseClubNum { get; set; }
		public decimal? PrefBudgetFromAmt { get; set; }
		public decimal? PrefBudgetToAmt { get; set; }
		public string CounselorLogin { get; set; }
		public DateTime? LastContactDate { get; set; }
		public DateTime? BirthdayDate { get; set; }
		public DateTime? AnniversaryDate { get; set; }
		public decimal? TotalSailedAmt { get; set; }
		public decimal? TotalNonCruiseAmt { get; set; }
		public DateTime? AddedToListDate { get; set; }
		public DateTime? LastSailDate { get; set; }
		public string CustomerComments { get; set; }
		public int? MktgSrcId { get; set; }
		public string Gender { get; set; }

		public short? PreTitleCode { get; set; }
		public short? PostTitleCode { get; set; }
		public short? PrefDurationFrom { get; set; }
		public short? PrefDurationTo { get; set; }
		public short? NumNonCruises { get; set; }
		public short? NumSailedCruises { get; set; }
		public short? NumTripsLess7Days { get; set; }
		public short? NumTrips7Days { get; set; }
		public byte? Age { get; set; }
		public short? NumSailedOtherStore { get; set; }
	}
}
