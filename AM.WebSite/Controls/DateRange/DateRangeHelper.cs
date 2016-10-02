using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.DateRange;
using AM.WebSite.Controls.DateRange.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region DateRange
		public static MvcHtmlString PSDateRange(this HtmlHelper htmlHelper, string id)
		{
			DateRangeModel requestModel = new DateRangeModel
			{
				ID = id,
				RangeOptions = RangeOptions.All,
				Title = "Dates"
			};

			DateRangeModel model = DateRangeController.GetDateRangeModel(requestModel);

			return htmlHelper.Partial("~/Controls/DateRange/Views/DateRange.cshtml", model);
		}

		public static MvcHtmlString PSDateRange(this HtmlHelper htmlHelper, DateRangeModel requestModel)
		{
			DateRangeModel model = DateRangeController.GetDateRangeModel(requestModel);

			return htmlHelper.Partial("~/Controls/DateRange/Views/DateRange.cshtml", model);
		}


		public static MvcHtmlString PSDateRange(this HtmlHelper htmlHelper, string id, DateRangeModel requestModel)
		{
			DateRangeModel model = DateRangeController.GetDateRangeModel(requestModel);
			model.ID = id;
			return htmlHelper.Partial("~/Controls/DateRange/Views/DateRange.cshtml", model);
		}

		public static MvcHtmlString PSDateRange(this HtmlHelper htmlHelper)
		{
			var model = DateRangeController.GetDateRangeModel(new DateRangeModel(RangeOptions.All, DateRangeCode.ThisWeek, "Dates"));

			return htmlHelper.Partial("~/Controls/DateRange/Views/DateRange.cshtml", model);
		}
		#endregion
	}
}