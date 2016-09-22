using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL.Entities.Business
{
	public class ResPas
	{
		[Key, Column(Order = 0)]
		public int ReservationId { get; set; }
		[Key, Column(Order = 1)]
		public int PassengerId { get; set; }

		public int PaxSeq { get; set; }
		public short? DietCode { get; set; }
		public short? DiningPrefCode { get; set; }
		public short? MktgSrcId { get; set; }
		public short? SpecialOccCode { get; set; }
		public DateTime? SpecialOccDate { get; set; }
        public DateTime? SpecialOccObsDate { get; set; }
        public bool Smoker { get; set; }
		public short? TableSizeCode { get; set; }
		public string SpecialServiceDesc { get; set; }
		public DateTime? PaxReturnDate { get; set; }
		public DateTime? PaxDepartDate { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string EditBy { get; set; }
		public DateTime? EditDate { get; set; }
		public string TableNumber { get; set; }
		public string CounselorLogin { get; set; }
		public string GrpAssociateType { get; set; }
		public int? GrpAssociateId { get; set; }
		public string DiningComments { get; set; }
		public string VisaStatus { get; set; }
		public DateTime? BookedDate { get; set; }
		public int UniqueTravelerId { get; set; }
		public string GrpAssociateAgentLogin { get; set; }
		public int? GrpAssociateAgentId { get; set; }
		public string GeneralComments { get; set; }

		public virtual Customer Customer { get; set; }

	}
}
