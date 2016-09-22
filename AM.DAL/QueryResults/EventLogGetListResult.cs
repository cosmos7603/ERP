using System.ComponentModel.DataAnnotations;
using System;

namespace AM.DAL.QueryResults
{
	public class EventLogGetListResult
	{
        public int EventLogId { get; set; }
        public DateTime EventDate { get; set; }
        public string Login { get; set; }
		public string EventCode { get; set; }
		public string LogLevel { get; set; }
		public string Description { get; set; }
	}
}
