using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
    public class StoreDestination
    {
        [Key]
        public int StoreDestinationId { get; set; }
        public int StoreId { get; set; }
        public int DestinationId { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }

		[ForeignKey("DestinationId")]
		public virtual Destination Destination { get; set; }

		[ForeignKey("StoreId")]
		public virtual Store Store { get; set; }
	}
}