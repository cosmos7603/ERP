using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public class StoreNews : AuditableEntity
	{
		[Key]
		public int StoreNewsId { get; set; }
		public int StoreId { get; set; }
		public DateTime StoreNewsDate { get; set; }
		public string StoreNewsTitle { get; set; }
		public string StoreNewsDesc { get; set; }
		public bool Active { get; set; }
		public int? Attach1FileId { get; set; }
		public int? Attach2FileId { get; set; }

		[ForeignKey("Attach1FileId")]
		public DataFile Attach1 { get; set; }
		[ForeignKey("Attach2FileId")]
		public DataFile Attach2 { get; set; }

	}
}