using System;

namespace AM.DAL.QueryResults
{
    public class DashboardGetAutoCompleteSearchResult
    {
        public int? ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? GrpId { get; set; }
        public string Name { get; set; }
        public string DesignationLetter { get; set; }
    }

}
