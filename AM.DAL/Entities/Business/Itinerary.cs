using AM.DAL.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Itinerary : AuditableEntity
    {
        [Key]
        public int ItineraryId { get; set; }
        public int StoreId { get; set; }
        public string ItineraryName { get; set; }
        public short ShipId { get; set; }
        public int DestinationId { get; set; }
        public short DurationNum { get; set; }
        public string EmbarkCityName { get; set; }
        public string DebarkCityName { get; set; }
        public bool Deleted { get; set; }
        public int? PpoDepartureId { get; set; }
        public string LegacyItineraryCode { get; set; }
        public bool Locked { get; set; }
        public short DatelineNum { get; set; }
        public string VendorItineraryName { get; set; }
        public int? PpoOfferId { get; set; }
        public short WsDurationNum { get; set; }

		public virtual Destination Destination { get; set; }
        public virtual Ship Ship { get; set; }
    }
}
