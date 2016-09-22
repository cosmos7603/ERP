using System;
using System.ComponentModel.DataAnnotations;

namespace AM.DAL
{
	public partial class DataFile
	{
		[Key]
		public int DataFileId { get; set; }
		public string FileName { get; set; }
		public string Extension { get; set; }
		public string SourceCode { get; set; }
		public int DataContentId { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }

		public DataContent DataContent { get; set; }
	}
}
