using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AM.WebSite.Controls.DataFileList.Models
{
	public class DataFileListItem
	{
		public string FileName { get; set; }
		public string DataFileKey { get; set; }
	}
}