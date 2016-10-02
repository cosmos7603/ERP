using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.MonthYearSelection;
using AM.WebSite.Controls.MonthYearSelection.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region PSMonthYearSelection
		public static MvcHtmlString PSMonthYearSelection(this HtmlHelper htmlHelper, string id)
		{
			MonthYearSelectionModel requestModel = new MonthYearSelectionModel
			{
				ID = id,
				MonthYearOptions = MonthYearSelectionOptions.Last5Years
			};

			MonthYearSelectionModel model = MonthYearSelectionController.GetMonthYearSelectionModel(requestModel);

			return htmlHelper.Partial("~/Controls/MonthYear/Views/MonthYear.cshtml", model);
		}

		public static MvcHtmlString PSMonthYearSelection(this HtmlHelper htmlHelper, MonthYearSelectionModel requestModel)
		{
			MonthYearSelectionModel model = MonthYearSelectionController.GetMonthYearSelectionModel(requestModel);

			return htmlHelper.Partial("~/Controls/MonthYear/Views/MonthYear.cshtml", model);
		}
		#endregion
	}
}