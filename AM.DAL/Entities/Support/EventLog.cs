using System;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public partial class EventLog
    {
		[Key]
		public int EventLogId { get; set; }
		public DateTime EventDate { get; set; }
		public string Login { get; set; }
		public string LogLevel { get; set; }
		public string EventCode { get; set; }
		public string Description { get; set; }
		public string Exception { get; set; }
		public string Payload { get; set; }
		public string Context { get; set; }
	}
}
