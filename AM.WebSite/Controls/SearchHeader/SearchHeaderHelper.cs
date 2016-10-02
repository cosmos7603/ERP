using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.SearchHeader.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region SearchHeader
		public static MvcHtmlString PSSearchHeader(this HtmlHelper htmlHelper, string title)
		{
			return htmlHelper.Partial("~/Controls/SearchHeader/Views/SearchHeader.cshtml", new SearchHeaderModel(title));
		}

		public static MvcHtmlString PSSearchHeader(this HtmlHelper htmlHelper)
		{
			return htmlHelper.Partial("~/Controls/SearchHeader/Views/SearchHeader.cshtml", new SearchHeaderModel());
		}

		public static MvcHtmlString PSSearchHeader(this HtmlHelper htmlHelper, SearchHeaderModel model)
		{
			return htmlHelper.Partial("~/Controls/SearchHeader/Views/SearchHeader.cshtml", model);
		}
		#endregion
	}
}