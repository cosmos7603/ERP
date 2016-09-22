using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class LeadReservation
    {
        [Key, Column(Order = 0)]
        public int LeadId { get; set; }
        [Key, Column(Order = 1)]
        public int ReservationId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
