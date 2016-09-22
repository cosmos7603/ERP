using System;
using AM.Services;
using AM.WebSite.Areas.Shared.Header.Models;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Footer
		public static MvcHtmlString AMHeader(this HtmlHelper htmlHelper)
		{
			return htmlHelper.Partial("~/Areas/Shared/Header/Views/_TopNavBar.cshtml");
		}

		public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null)
		{

			if (String.IsNullOrEmpty(cssClass))
				cssClass = "active";

			string currentAction = (string)html.ViewContext.RouteData.Values["action"];
			string currentController = (string)html.ViewContext.RouteData.Values["controller"];

			if (String.IsNullOrEmpty(controller))
				controller = currentController;

			if (String.IsNullOrEmpty(action))
				action = currentAction;

			return controller.ToLower() == currentController.ToLower() && action.ToLower() == currentAction.ToLower() ?
				cssClass : String.Empty;
		}

		public static string PageClass(this HtmlHelper html)
		{
			string currentAction = (string)html.ViewContext.RouteData.Values["action"];
			return currentAction;
		}
		#endregion
	}
}