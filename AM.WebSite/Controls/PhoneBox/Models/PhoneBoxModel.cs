using System.Collections.Generic;

namespace AM.WebSite.Controls.PhoneBox.Models
{
	public class PhoneBoxModel
	{
		public string ID { get; set; }
		public string Value { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }
	}
}