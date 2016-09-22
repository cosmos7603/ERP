namespace AM.DAL.QueryResults
{
	public class CabinCategoryResult : AuditableEntity
	{
		public short ShipId { get; set; }
		public string CabinCatCode { get; set; }
		public int StoreId { get; set; }
		public short? CabinCatSeq { get; set; }
		public string CabinDesc { get; set; }
		public bool Deleted { get; set; }
		public int? StateroomType { get; set; }
		public string StateroomTypeName { get; set; }
	}
}
