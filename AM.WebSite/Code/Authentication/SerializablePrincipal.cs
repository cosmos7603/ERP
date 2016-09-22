namespace AM.WebSite.Security
{
	public class SerializablePrincipal
	{
		public string Login { get; set; }
		public int? StoreId { get; set; }
		public string CorpBrandCode { get; set; }
		public string CorpHoCode { get; set; }
    }
}