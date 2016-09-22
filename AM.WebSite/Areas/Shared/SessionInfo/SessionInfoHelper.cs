using AM.Utils;
using AM.WebSite.Areas.Shared.SessionInfo;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region SessionInfo
		public static MvcHtmlString AMSessionInfo(this HtmlHelper htmlHelper)
		{
			return htmlHelper.Partial("~/Areas/Shared/SessionInfo/Views/SessionInfo.cshtml", SessionInfoController.GetSessionInfoModel());
		}
		#endregion
	}
}