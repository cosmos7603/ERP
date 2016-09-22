using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class PPOItineraryDay
    {
        public int ItinDayId { get; set; }
        public int OfferId { get; set; }
        public int Item { get; set; }
        public string ItemDay { get; set; }
        public string Arrive { get; set; }
        public string Depart { get; set; }
        public string Caption { get; set; }
        public int? LocationId { get; set; }
    }
}
