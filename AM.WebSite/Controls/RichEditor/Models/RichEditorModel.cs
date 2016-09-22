using System.Collections.Generic;

namespace AM.WebSite.Controls.RichEditor.Models
{
	public class RichEditorModel
	{
		public string ID { get; set; }
		public string Content { get; set; }
		public bool Required { get; set; }
		public IDictionary<string, object> HtmlAttributes { get; set; }
	}
}