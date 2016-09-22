using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AM.DAL.Entities.Business
{
	public class Vendor
	{
		[Key]
		public int VendorId { get; set; }
		public int StoreId { get; set; }
		public string VendorName { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string StateName { get; set; }
		public string Zip { get; set; }
		public string CountryCode { get; set; }
		public string CurrencyCode { get; set; }
		public string MainPhoneNum { get; set; }
		public string ResPhoneNum { get; set; }
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
		public bool Deleted { get; set; }
		public bool Active { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public string EditBy { get; set; }
		public DateTime? EditDate { get; set; }
		public string CustSrvcPhone { get; set; }
		public string CustSrvcFax { get; set; }
		public string DsmCell { get; set; }
		public string DsmEmail { get; set; }
		public short? ReceiptType { get; set; }
		public string VendorTypeCode { get; set; }
		public bool PreventUccv { get; set; }
		public bool? CentralizedBookings { get; set; }
		public string VendorImageUrl { get; set; }
		public bool ForceCentralized { get; set; }
		public bool GiftManagement { get; set; }
		public bool Clone { get; set; }
		public int? CloneParentId { get; set; }
		public string CloneTypeCode { get; set; }
		public string InsuranceEngineCode { get; set; }
		public string VendorCode { get; set; }
		public int? PpoVendorId { get; set; }
		public bool Cruisepro { get; set; }
		public string VendorLogoUrl { get; set; }

		public virtual Store Store { get; set; }
		public virtual List<VendorInfo> VenforInfos { get; set; }
	}
}
