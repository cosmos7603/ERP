namespace AM.DAL.QueryResults
{
	public class VendorResult
	{
		public int VendorId { get; set; }
		public int StoreId { get; set; }
		public string VendorName { get; set; }
		public string CurrencyCode { get; set; }
		public bool Deleted { get; set; }
		public bool Active { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string StateName { get; set; }
		public string Zip { get; set; }
		public string CountryCode { get; set; }
		public string MainPhoneNum { get; set; }
		public string ResPhoneNum { get; set; }
		public string GroupPhoneNum { get; set; }
		public string MainFax { get; set; }
		public string ResFax { get; set; }
		public string GroupFax { get; set; }
		public string MainEmail { get; set; }
		public string GroupEmail { get; set; }
		public string DsmPhone { get; set; }
		public string DsmFax { get; set; }
		public string DsmName { get; set; }
		public string DsmAddress1 { get; set; }
		public string DsmAddress2 { get; set; }
		public string DsmCity { get; set; }
		public string DsmStateName { get; set; }
		public string DsmZip { get; set; }
		public string DsmCountryCode { get; set; }
		public bool CentralizedBookings { get; set; }
		public bool ForceCentralized { get; set; }
		public string InsuranceEngineCode { get; set; }
	}
}
