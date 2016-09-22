using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.DAL.Entities.Business;

namespace AM.DAL
{
    public class Interest : AuditableEntity
    {
        [Key]
        public int InterestId { get; set; }
        public int InterestCatId { get; set; }
        public int StoreId { get; set; }
        public string CounselorLogin { get; set; }
        public string InterestName { get; set; }
        public bool Deleted { get; set; }
        public string HoCode { get; set; }
        public string InterestLevelCode { get; set; }

		[ForeignKey("InterestCatId")]
		public virtual InterestCat InterestCat { get; set; }

		public virtual List<BrandInterest> BrandInterests { get; set; }

		public Interest()
		{
			BrandInterests = new List<BrandInterest>();
		}
	}
}
