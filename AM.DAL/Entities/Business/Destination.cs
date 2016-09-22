using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public class Destination : AuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DestinationId { get; set; }
        public int StoreId { get; set; }
        public string DestinationName { get; set; }
        public short DestAreaId { get; set; }
        public bool Deleted { get; set; }
        public int? PpoDestinationId { get; set; }

		[ForeignKey("DestAreaId")]
		public virtual DestinationArea DestinationArea { get; set; }

		[ForeignKey("StoreId")]
		public virtual Store Store { get; set; }
	}
}
