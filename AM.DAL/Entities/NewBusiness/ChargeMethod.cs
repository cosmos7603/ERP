using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public  class ChargeMethod : AuditableEntity
	{

		[Display(Name = "Medio de Cobro")]
		public string Description { get; set; }
	
	}
}