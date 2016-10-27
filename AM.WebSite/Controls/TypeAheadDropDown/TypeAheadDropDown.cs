using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
	
		public static MvcHtmlString PSTypeAheadDropDown(this HtmlHelper htmlHelper, string name, string dropDownOption, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "form-control");

			newAttributes.Add("data-provide", "typeahead");

			return htmlHelper.DropDownList(name, null, newAttributes);
		}
	
	}
}