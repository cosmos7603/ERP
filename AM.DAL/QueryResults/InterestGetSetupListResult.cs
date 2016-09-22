using System.ComponentModel.DataAnnotations;

namespace AM.DAL.SPEntities
{
    public class InterestGetSetupListResult
	{
        [Key]
        public int InterestId { get; set; }
        public int InterestCatId { get; set; }
        public string InterestLevelCode { get; set; }
        public string HoCode { get; set; }
        public int StoreId { get; set; }
        public string InterestName { get; set; }
        public string CounselorLogin { get; set; }
        public string InterestCatName { get; set; }
		public bool Deleted { get; set; }
	}

}
