using AM.Utils;
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
		#region PSMonthYearBox
		public static MvcHtmlString PSMonthYearBox(this HtmlHelper htmlHelper, string name)
		{
			return PSMonthYearBox(htmlHelper, name, null);
		}

		public static MvcHtmlString PSMonthYearBox(this HtmlHelper htmlHelper, string name, string value)
		{
			return PSMonthYearBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSMonthYearBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-month-year-box form-control");

			string textBoxString = htmlHelper.TextBox(name,value, newAttributes).ToHtmlString();

			return new MvcHtmlString(textBoxString);
		}
		#endregion
	}
}