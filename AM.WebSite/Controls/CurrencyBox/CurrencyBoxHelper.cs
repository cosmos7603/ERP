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
		#region CurrencyBox
		public static MvcHtmlString PSCurrencyBox(this HtmlHelper htmlHelper, string name)
		{
			return PSCurrencyBox(htmlHelper, name, "", null);
		}

		public static MvcHtmlString PSCurrencyBox(this HtmlHelper htmlHelper, string name, object value)
		{
			return PSCurrencyBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSCurrencyBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-currency-box form-control text-right")
				.AddClass("maxlength", "18");

			decimal amount = value.ToDecimal();

			return htmlHelper.TextBox(name, Formatting.FormatCurrency(amount), newAttributes);
		}
		#endregion
	}
}