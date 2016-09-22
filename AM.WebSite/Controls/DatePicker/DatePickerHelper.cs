using AM.Utils;
using AM.WebSite.Controls.DatePicker.Models;
using AM.WebSite.MVC;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region DatePicker
		public static MvcHtmlString PSDatePicker(this HtmlHelper htmlHelper, string name)
		{
			return PSDatePicker(htmlHelper, name, null, null);
		}

		public static MvcHtmlString PSDatePicker(this HtmlHelper htmlHelper, string name, DateTime? value)
		{
			return PSDatePicker(htmlHelper, name, value, null);
		}

		public static MvcHtmlString PSDatePicker(this HtmlHelper htmlHelper, string name, DateTime? value, object htmlAttributes)
		{
			var newAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes);

			newAttributes = newAttributes
				.AddClass("class", "ps-date-picker form-control text-right")
				.AddClass("maxlength", "11");

				// Build Model
				var model = new DatePickerModel
				{
					ID = name,
					Value = value,
					DisplayValue = Formatting.FormatDate(value),
                    HtmlAttributes = newAttributes
				};

			return htmlHelper.Partial("~/Controls/DatePicker/Views/DatePicker.cshtml", model);
		}
		#endregion
	}
}