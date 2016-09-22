using System;

namespace AM.DAL.QueryResults
{
    public class DashboardGetReportNotificationsResult
	{
		public DateTime ExecutionDate { get; set; }
		public string ReportEmailName { get; set; }
		public int  ReportEmailExecutionId { get; set; }
	    public int Notified { get; set; }
	}

}
