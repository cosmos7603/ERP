using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public class DeliveryNote : AuditableEntity
	{
		[Display(Name = "N° Remito")]
		public string Code { get; set; }
	}
}