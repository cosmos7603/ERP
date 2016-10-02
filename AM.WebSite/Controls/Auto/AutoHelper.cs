using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using AM.WebSite.Controls.Auto.Models;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Auto
		public static MvcHtmlString PSAuto(this HtmlHelper htmlHelper, string name, string action)
		{
			return PSAuto(htmlHelper, name, action, null);
		}

		public static MvcHtmlString PSAuto(this HtmlHelper htmlHelper, string name, string action, object htmlAttributes)
		{
			return PSAuto(htmlHelper, new AutoModel
			{
				ID = name,
				Action = action,
				MinLength = 2,
                DelayMs = 0,
				Enabled = true,
				HtmlAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes)
			});
		}

		public static MvcHtmlString PSAuto(this HtmlHelper htmlHelper, AutoModel model)
		{
			if (model.HtmlAttributes == null)
				model.HtmlAttributes = new RouteValueDictionary();

			model.HtmlAttributes = model.HtmlAttributes
				.AddClass("class", "ps-auto form-control");

			// Add class name
			if (!model.Enabled)
				model.HtmlAttributes.Add(new KeyValuePair<string, object>("disabled", "disabled"));

			return htmlHelper.Partial("~/Controls/Auto/Views/Auto.cshtml", model);
		}
		#endregion
	}
}