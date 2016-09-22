using AM.Utils;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Footer
		public static MvcHtmlString AMFooter(this HtmlHelper htmlHelper)
		{
			return htmlHelper.Partial("~/Areas/Shared/Footer/Views/Footer.cshtml");
		}
		#endregion
	}
}