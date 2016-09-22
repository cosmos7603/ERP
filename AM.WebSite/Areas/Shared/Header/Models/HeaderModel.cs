using System.Collections.Generic;
using System.Web.Mvc;

namespace AM.WebSite.Areas.Shared.Header.Models
{
	public class HeaderModel
    {
		public bool PublicMode { get; set; }
		public string UserName { get; set; }
		public string UserLegend { get; set; }
        public string HeaderColorClass { get; set; }
	}
}