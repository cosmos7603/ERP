using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AM.Utils;

namespace AM.WebSite.Controls.PhoneBox.Models
{
	public class PhoneBoxModel
	{
		public string ID { get; set; }
		public string Value { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }
	}
}