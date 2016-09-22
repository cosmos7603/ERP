using AM.WebSite.Controls.Combo.Models;
using AM.WebSite.MVC;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Combo
		public static MvcHtmlString PSCombo(this HtmlHelper htmlHelper, string name, SelectList selectList)
		{
			return PSCombo(htmlHelper, name, selectList, "", null);
		}

		public static MvcHtmlString PSCombo(this HtmlHelper htmlHelper, string name, SelectList selectList, string dropDownOption)
		{
			return PSCombo(htmlHelper, name, selectList, dropDownOption, null);
		}

		public static MvcHtmlString PSCombo(this HtmlHelper htmlHelper, string name, SelectList selectList, object htmlAttributes)
		{
			return PSCombo(htmlHelper, name, selectList, "", htmlAttributes);
		}

		public static MvcHtmlString PSCombo(this HtmlHelper htmlHelper, string name, SelectList selectList, string dropDownOption, object htmlAttributes)
		{
			var model = new ComboModel
			{
				ID = name,
				DataSource = selectList,
				HtmlAttributes = ControlHelper.GetHtmlAttributes(htmlAttributes),
				DropDownOption = dropDownOption,
				SearchBox = true
			};

			return PSCombo(htmlHelper, model);
		}

		public static MvcHtmlString PSCombo(this HtmlHelper htmlHelper, ComboModel model)
		{
			if (model.HtmlAttributes == null)
				model.HtmlAttributes = new RouteValueDictionary();

			model.HtmlAttributes = model.HtmlAttributes
				.AddClass("class", "ps-combo form-control select2");

			// Required, in case combo starts in a hidden DIV
			model.HtmlAttributes = model.HtmlAttributes
				.AddClass("style", "width: 100%");

			return htmlHelper.Partial("~/Controls/Combo/Views/Combo.cshtml", model);
		}
		#endregion
	}
}