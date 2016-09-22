using AM.Utils;
using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region IntBox
		public static MvcHtmlString PSIntBox(this HtmlHelper htmlHelper, string name)
		{
			return PSIntBox(htmlHelper, name, "", null);
		}

		public static MvcHtmlString PSIntBox(this HtmlHelper htmlHelper, string name, object value)
		{
			return PSIntBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSIntBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-int-box form-control text-right")
				.AddClass("maxlength", "8");

			string textBoxString = htmlHelper.TextBox(name, value, newAttributes).ToHtmlString();

			return new MvcHtmlString(textBoxString);
		}
		#endregion
	}
}