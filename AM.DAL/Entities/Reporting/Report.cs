using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public partial class Report
    {
		[Key]
		public int ReportId { get; set; }
		public string ReportTitle { get; set; }
		public string TypeName { get; set; }
	}
}
