using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region PSDropDown
		public static MvcHtmlString PSDropDown(this HtmlHelper htmlHelper, string name, SelectList selectList)
		{
			return PSDropDown(htmlHelper, name, selectList, "", null);
		}

		public static MvcHtmlString PSDropDown(this HtmlHelper htmlHelper, string name, SelectList selectList, string dropDownOption)
		{
			return PSDropDown(htmlHelper, name, selectList, dropDownOption, null);
		}

		public static MvcHtmlString PSDropDown(this HtmlHelper htmlHelper, string name, SelectList selectList, object htmlAttributes)
		{
			return PSDropDown(htmlHelper, name, selectList, "", htmlAttributes);
		}

		public static MvcHtmlString PSDropDown(this HtmlHelper htmlHelper, string name, SelectList selectList, string dropDownOption, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-dropdown-box form-control");

            return htmlHelper.DropDownList(name, selectList, dropDownOption.Equals(DropDownOption.REMOVE) ? null : dropDownOption, newAttributes);
		}
		#endregion
	}
}