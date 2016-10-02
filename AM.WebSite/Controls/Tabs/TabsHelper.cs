using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.Tabs.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Tabs
		public static MvcHtmlString PSTabs(this HtmlHelper htmlHelper, params string[] titles)
		{
			var tabs = titles.Select(x => new TabItem(x)).ToList();
			return htmlHelper.PSTabs(tabs);
		}

		public static MvcHtmlString PSTabs(this HtmlHelper htmlHelper, List<TabItem> tabs)
		{
			var model = new TabsModel { ID = "tabs", Tabs = tabs };

			if (tabs.Count > 0)
				tabs[0].Active = true;

			return htmlHelper.PSTabs(model);
		}

		public static MvcHtmlString PSTabs(this HtmlHelper htmlHelper, TabsModel model)
		{
			return htmlHelper.Partial("~/Controls/Tabs/Views/Tabs.cshtml", model);
		}
		#endregion
	}
}