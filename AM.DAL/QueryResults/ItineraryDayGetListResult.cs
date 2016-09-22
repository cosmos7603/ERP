using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class ItineraryDayGetListResult
    {
        public int? ItineraryId { get; set; }
        public short? ItineraryDaySeq { get; set; }
        public short? ItineraryDayNum { get; set; }
        public string ItineraryDayStr { get; set; }
        public string PortName { get; set; }
        public string ArriveTimeDate { get; set; }
        public string DepartTimeDate { get; set; }
        public int? StoreId { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime ItineraryDayDate { get; set; }
        public short ShipId { get; set; }
    }
}
