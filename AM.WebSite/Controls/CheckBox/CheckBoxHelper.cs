using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region PSCheckBox
		public static MvcHtmlString PSCheckBox(this HtmlHelper htmlHelper, string name)
		{
			return PSCheckBox(htmlHelper, name, false, null);
		}

		public static MvcHtmlString PSCheckBox(this HtmlHelper htmlHelper, string name, bool status)
		{
			return PSCheckBox(htmlHelper, name, status, null);
		}

		public static MvcHtmlString PSCheckBox(this HtmlHelper htmlHelper, string name, bool status, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

            //newAttributes = newAttributes.AddClass("class", "i-checks");

            string checkBoxWithHidden = htmlHelper.CheckBox(name, status, newAttributes).ToHtmlString().Trim();
			string pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1));
			return new MvcHtmlString(pureCheckBox);
		}
		#endregion
	}
}