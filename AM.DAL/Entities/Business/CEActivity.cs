using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public class CEActivity : AuditableEntity
	{
		[Key]
		public int CeActivityId { get; set; }
		public string CeActivityCode { get; set; }
		public string CeActivityName { get; set; }
		public string CeActivityLevelCode { get; set; }
		public string CeActivityTypeCode { get; set; }
		public bool NonCruise { get; set; }
		public bool Deleted { get; set; }
		public int? ImageFileId { get; set; }

		#region Enums
		public enum CEActivityType
		{
			PRINT,
			EMAIL,
			SHORE
		}

		public enum CEActivityLevel
		{
			RES,
			CLI
		}

		public enum CEActivityWhenCode
		{
			BEFORE,
			AFTER
		}
		#endregion
	}
}
