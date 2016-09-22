using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AM.WebSite.Controls.DataFileUpload.Models
{
    public class DataFileUploadModel
	{
        public string ID { get; set; }
		public int DataFileId { get; set; }
		public string DataFileKey { get; set; }
		public string FileName { get; set; }
		public string Extensions { get; set; }
		public bool Multiple { get; set; }

        public List<int> ListDataFileId { get; set; } = new List<int>();
        public List<string> ListDataFileKey { get; set; } = new List<string>();
        public List<string> ListFileName { get; set; } = new List<string>();
    }
}