using System.Collections.Generic;

namespace AM.WebSite.Controls.EmailBox.Models
{
	public class EmailBoxModel
	{
		public string ID { get; set; }
		public string Value { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }
	}
}