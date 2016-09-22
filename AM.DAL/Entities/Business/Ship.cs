using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL.Entities.Business
{
	public class Ship : AuditableEntity
	{
		[Key, Column(Order = 0)]
		public short ShipId { get; set; }
		public int StoreId { get; set; }
		public string ShipName { get; set; }
		public int CruiseLineId { get; set; }
		public bool Deleted { get; set; }
		public int? PpoShipId { get; set; }
		public string ShipImageUrl { get; set; }
		public string ShoreExcGroupId { get; set; }
	}
}
