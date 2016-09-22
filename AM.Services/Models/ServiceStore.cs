namespace AM.Services.Models
{
	public class ServiceStore
	{
		public int StoreId { get; set; }
		public string StoreName { get; set; }
		public string InvoiceStoreName { get; set; }
		public string BrandCode { get; set; }
		public bool FullVersion { get; set; }
		public bool EnableBranches { get; set; }
		public bool Virtuoso { get; set; }
		public bool GstPricings { get; set; }
	}
}
