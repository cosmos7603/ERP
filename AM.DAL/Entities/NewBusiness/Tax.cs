using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public class Tax : AuditableEntity
	{
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
