using System;
using System.Collections.Generic;

namespace AM.DAL.QueryResults
{
	public class DashboardMktgGetListByUserResult
	{
		public int MktgActivityId { get; set; }
		public string MktgActivityName { get; set; }
		public DateTime DropDate { get; set; }
		public bool SearchAll { get; set; }
		public DateTime CreateDate { get; set; }
		public int? DataFileId { get; set; }
    }

}
