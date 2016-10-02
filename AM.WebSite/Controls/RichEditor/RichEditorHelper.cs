using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using AM.WebSite.Controls.RichEditor.Models;
using AM.WebSite.MVC;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region RichEditor
		public static MvcHtmlString PSRichEditor(this HtmlHelper htmlHelper, RichEditorModel model)
		{
			if (model.HtmlAttributes == null)
				model.HtmlAttributes = new RouteValueDictionary();

			if(model.Required)
			model.HtmlAttributes = model.HtmlAttributes
				.AddClass("class", "required jsRichEditor");

			return htmlHelper.Partial("~/Controls/RichEditor/Views/RichEditor.cshtml", model);
		}
		#endregion
	}
}