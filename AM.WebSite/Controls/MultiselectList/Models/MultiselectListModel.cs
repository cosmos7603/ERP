using System.Collections.Generic;
using System.Web.Mvc;

namespace AM.WebSite.Controls.MultiselectList.Models
{
	public class MultiselectListModel
	{
		public string ID { get; set; }
		public SelectList Items { get; set; }
		public List<string> SelectedValues { get; set; }
		public string Width { get; set; }
		public string Height { get; set; }
		public bool SearchBar { get; set; }
		public bool SelectAll { get; set; }

		public MultiselectListModel()
		{
			SelectAll = false;
			SearchBar = false;
			SelectedValues = new List<string>();
		}
	}
}