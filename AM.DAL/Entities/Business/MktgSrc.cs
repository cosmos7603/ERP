using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AM.DAL
{
    public class MktgSrc : AuditableEntity
	{
        [Key]
        public int MktgSrcId { get; set; }
        public short MktgCatId { get; set; }
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public string MktgSrcName { get; set; }
        public bool DefaultItem { get; set; }
        public bool Deleted { get; set; }
		public string HoCode { get; set; }
        public string MktgSrcLevelCode { get; set; }

		[ForeignKey("MktgCatId")]
		public virtual MktgCat MktgCat { get; set; }

		public virtual List<BrandMktgSrc> BrandMktgSrcs { get; set; }

		public MktgSrc()
		{
			BrandMktgSrcs = new List<BrandMktgSrc>();
		}
	}
}