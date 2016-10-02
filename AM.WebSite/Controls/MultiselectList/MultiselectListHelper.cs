using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.MultiselectList.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region MultiselectList
		public static MvcHtmlString PSMultiselectList(this HtmlHelper htmlHelper, MultiselectListModel model)
		{
			return htmlHelper.Partial("~/Controls/MultiselectList/Views/MultiselectList.cshtml", model);
		}
		#endregion
	}
}