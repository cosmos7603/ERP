using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.DAL.QueryResults
{
    public class RecentItemGetListResult
    {
        public int RecentItemId { get; set; }
        public string ItemTypeCode { get; set; }
        public int? ReservationId { get; set; }
        public int? GrpId { get; set; }
        public int? CustomerId { get; set; }
        public int? LeadId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? PriActBeginDate { get; set; }
        public string ShipName { get; set; }
        public string DesignationLetter { get; set; }
        public string CounselorLogin { get; set; }
    }
}
