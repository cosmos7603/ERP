using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public class Client : AuditableEntity
	{

		public Client()
		{
			//this.Sales = new HashSet<Sale>();
		}

		[Display(Name = "Cliente")]
		public string ComercialName { get; set; }
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Display(Name = "Observaciones")]
		public string Observations { get; set; }
		[Display(Name = "País")]
		public string Country { get; set; }
		[Display(Name = "Dirección")]
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		[Display(Name = "Provincia")]
		public string Province { get; set; }

		[Display(Name = "Ciudad")]
		public string City { get; set; }
		[Display(Name = "Código Postal")]
		public string ZipCode { get; set; }
		[Display(Name = "Teléfono")]
		public string Telephone1 { get; set; }
		[Display(Name = "Teléfono Alternativo")]
		public string Telephone2 { get; set; }
		[Display(Name = "CUIT")]
		public string CUIT { get; set; }
		[Display(Name = "Tipo de cliente")]
		public int ClientTypeId { get; set; }
		[Display(Name = "Nombre")]
		public string FirstName { get; set; }
		[Display(Name = "Apellido")]
		public string LastName { get; set; }
		[Display(Name = "DNI")]
		public string DNI { get; set; }
		[Display(Name = "Activo")]
		public bool Active { get; set; }

		public virtual ClientType ClientType { get; set; }

		//public virtual ICollection<Sale> Sales { get; set; }

	}
}