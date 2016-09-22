using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class Cruise : AuditableEntity
    {
        [Key]
        public int CruiseId { get; set; }
        public int ItineraryId { get; set; }
        public DateTime SailDate { get; set; }
        public int StoreId { get; set; }
        public bool Deleted { get; set; }
        public int? PpoDepartureId { get; set; }
    }
}
