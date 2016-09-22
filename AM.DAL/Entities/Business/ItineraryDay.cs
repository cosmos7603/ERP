using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL
{
    public class ItineraryDay : AuditableEntity
    {
        [Key]
        public int ItineraryId { get; set; }
        public short ItineraryDayNum { get; set; }
        public int StoreId { get; set; }
        public string PortName { get; set; }
        public short? DayOfWeek { get; set; }
        public string ArriveTimeDate { get; set; }
        public string DepartTimeDate { get; set; }
        public int? PpoItineraryDayId { get; set; }
        public short ItineraryDaySeq { get; set; }
        public string DatelineType { get; set; }
    }
}
