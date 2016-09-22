﻿using AM.WebSite.Controls.DataTable.Models;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region DataTable
		public static MvcHtmlString PSDataTable(this HtmlHelper htmlHelper, string id)
		{
			return htmlHelper.Partial("~/Controls/DataTable/Views/DataTable.cshtml", new DataTableModel { ID = id });
		}
		#endregion
	}
}