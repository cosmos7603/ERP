using System.ComponentModel.DataAnnotations;

namespace AM.DAL.Entities.Business
{
	public class ResStatus : AuditableEntity
	{
		[Key]
		public string StatusCode { get; set; }
		public string StatusName { get; set; }
		public string GrpStatusName { get; set; }
	}
}
