using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public class Client : AuditableEntity
	{
		[ScaffoldColumn(false)]
		public int Id { get; set; }
		public bool Active { get; set; }
		public string ComercialName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Observations { get; set; }
		public int ClientTypeId { get; set; }
		public string Country { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string ZipCode { get; set; }
		public string Telephone1 { get; set; }
		public string Telephone2 { get; set; }
		public string CUIT { get; set; }
		public string DNI { get; set; }

	}
}