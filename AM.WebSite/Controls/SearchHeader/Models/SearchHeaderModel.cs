using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AM.Utils;

namespace AM.WebSite.Controls.SearchHeader.Models
{
	public class SearchHeaderModel
	{
		public string ID { get; set; }
		public string Title { get; set; }

		public SearchHeaderModel()
		{
			ID = "SearchHeader";
			Title = "Search";
		}

		public SearchHeaderModel(string title)
		{
			ID = "SearchHeader";
			Title = title;
		}
	}
}