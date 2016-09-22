using AM.WebSite.Controls.EmailBox.Models;
using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region EmailBox
		public static MvcHtmlString PSEmailBox(this HtmlHelper htmlHelper, string name)
		{
			return PSEmailBox(htmlHelper, name, "", null);
		}

		public static MvcHtmlString PSEmailBox(this HtmlHelper htmlHelper, string name, string value)
		{
			return PSEmailBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSEmailBox(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-email-box form-control");

			// Build Model
			var model = new EmailBoxModel
			{
				ID = name,
				Value = value,
				HtmlAttributes = newAttributes
			};

			return htmlHelper.Partial("~/Controls/EmailBox/Views/EmailBox.cshtml", model);
		}
		#endregion
	}
}