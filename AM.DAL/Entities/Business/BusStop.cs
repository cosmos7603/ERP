using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class BusStop : AuditableEntity
    {
        [Key]
        public int BusStopId { get; set; }
        public int StoreId { get; set; }
        public string StopName { get; set; }
        public string StopAddress { get; set; }
        public bool Deleted { get; set; }
    }
}
