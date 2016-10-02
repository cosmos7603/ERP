using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region PSTextBox
		public static MvcHtmlString PSTextBox(this HtmlHelper htmlHelper, string name)
		{
			return PSTextBox(htmlHelper, name, null);
		}

		public static MvcHtmlString PSTextBox(this HtmlHelper htmlHelper, string name, object value)
		{
			return PSTextBox(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSTextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-text-box form-control");

			return htmlHelper.TextBox(name, value, newAttributes);
		}
		#endregion
	}
}