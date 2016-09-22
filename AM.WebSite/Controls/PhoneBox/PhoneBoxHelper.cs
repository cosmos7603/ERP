using AM.WebSite.Controls.PhoneBox.Models;
using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region PhoneBox
		public static MvcHtmlString PSPhoneBox(this HtmlHelper htmlHelper, string name)
		{
			return PSPhoneBox(htmlHelper, name, "", null);
		}

		public static MvcHtmlString PSPhoneBox(this HtmlHelper htmlHelper, string name, string value)
		{
			return PSPhoneBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSPhoneBox(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "form-control ps-phone-box");

			// Build Model
			var model = new PhoneBoxModel
			{
				ID = name,
				Value = value,
				HtmlAttributes = newAttributes
			};

			return htmlHelper.Partial("~/Controls/PhoneBox/Views/PhoneBox.cshtml", model);
		}
		#endregion
	}
}