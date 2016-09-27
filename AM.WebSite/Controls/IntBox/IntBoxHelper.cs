using AM.Utils;
using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using AM.WebSite.Controls.IntBox.Models;

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
				.AddClass("class", "touchspin .ps-int-box form-control")
				.AddClass("maxlength", "8");

			string textBoxString = htmlHelper.TextBox(name, value, newAttributes).ToHtmlString();
			string jsFunction = @" <script type='text/javascript'>
			$(document).ready(function()
			{
				$('.touchspin').TouchSpin({
							buttondown_class: 'btn btn-white',
					buttonup_class:
							'btn btn-white'
				});
					});
		</script>";
			textBoxString += jsFunction;

			return new MvcHtmlString(textBoxString);
		}

		//public static MvcHtmlString PSIntBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
		//{
		//	var model = new IntBoxModel
		//	{
		//		Name = name,
		//		Value = value
		//	};
		//	return htmlHelper.Partial("~/Controls/IntBox/Views/IntBox.cshtml",model);
		//}
		#endregion
	}
}