using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AM.DAL
{
	public partial class ReportRun
    {
		[Key]
		public int ReportRunId { get; set; }
		public int ReportId { get; set; }
		public DateTime RunDate { get; set; }
		public DateTime EndDate { get; set; }
		public string ParamsDescription { get; set; }
		public int UserId { get; set; }
		public int DataFileId { get; set; }
		
		[ForeignKey("DataFileId")]
		public virtual DataFile DataFile { get; set; }
	}
}
