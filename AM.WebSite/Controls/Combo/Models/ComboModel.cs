using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AM.WebSite.Controls.Combo.Models
{
	public class ComboParam
	{
		public string Name { get; set; }
		public string Source { get; set; }
	}

	public class ComboModel
	{
		public string ID { get; set; }
		public SelectList DataSource { get; set; }
		public string DropDownOption { get; set; }
		public string Action { get; set; }
		public bool SearchBox { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }

		public ComboModel()
		{
			DataSource = new SelectList(Enumerable.Empty<SelectListItem>());
			SearchBox = true;
        }
	}
}