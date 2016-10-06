using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public  class SaleCategory: AuditableEntity
	{
		
		public SaleCategory()
		{
			//this.Sales = new HashSet<Sale>();
		}

		[Display(Name = "Categoría")]
		public string Description { get; set; }

		//public virtual ICollection<Sale> Sales { get; set; }
	}
}