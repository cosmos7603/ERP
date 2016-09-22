using System;

namespace AM.DAL.QueryResults
{
    public class DashboardGetMarketingNewsResult
    {
        public int MktgActivityId { get; set; }
        public string MktgActivityName { get; set; }
        public DateTime DropDate { get; set; }
        public bool SearchAll { get; set; }
        public DateTime CreateDate { get; set; }
        public int Notified { get; set; }
        public int Reviewed { get; set; }
    }

}
