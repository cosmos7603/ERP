using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.Utils;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Members
		private const int DefaultDP = 2;
		#endregion

		#region DecimalBox
		public static MvcHtmlString PSDecimalBox(this HtmlHelper htmlHelper, string name)
		{
			return PSDecimalBox(htmlHelper, name, "", DefaultDP, null);
		}

		public static MvcHtmlString PSDecimalBox(this HtmlHelper htmlHelper, string name, object value)
		{
			return PSDecimalBox(htmlHelper, name, value, DefaultDP, null);
		}

		public static MvcHtmlString PSDecimalBox(this HtmlHelper htmlHelper, string name, object value, int dp)
		{
			return PSDecimalBox(htmlHelper, name, value, dp, null);
		}

		public static MvcHtmlString PSDecimalBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			return PSDecimalBox(htmlHelper, name, value, DefaultDP, htmlAttributes);
		}

		public static MvcHtmlString PSDecimalBox(this HtmlHelper htmlHelper, string name, object value, int dp, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			decimal n = value.ToDecimal();
			string field = "";

			newAttributes = newAttributes
				.AddClass("class", "ps-decimal-box form-control text-right")
				.AddClass("maxlength", "8")
				.AddClass("data-dp", dp.ToString());

			if (value != null && value.ToString() != "")
				field = Formatting.FormatDecimal(n, dp);

			return htmlHelper.TextBox(name, field, newAttributes);
		}
		#endregion
	}
	}