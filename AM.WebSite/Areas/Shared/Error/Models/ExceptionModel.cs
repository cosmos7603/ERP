using System;

namespace AM.WebSite.Areas.Shared.Error.Models
{
	public class ExceptionModel
	{
		public string TrackingCode { get; set; }
		public string Message { get; set; }
		public string StackTrace { get; set; }
	}
}