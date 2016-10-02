using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AM.WebSite.Controls.DateRange.Models;
using AM.WebSite.Shared.Controllers;

namespace AM.WebSite.Controls.DateRange
{
	[Route("Controls/DateRange/{action=index}")]
	[ViewsPath("~/Controls/DateRange/Views")]
	public class DateRangeController : BaseController
	{
		#region Partial View
		[HttpPost]
		public ActionResult DateRange(DateRangeModel requestModel)
		{
			var model = GetDateRangeModel(requestModel);

			return PartialView(model);
		}
		#endregion

		#region Models
		public static DateRangeModel GetDateRangeModel(DateRangeModel model)
		{
			switch (model.RangeOptions)
			{
				case RangeOptions.All:
					model.DateRangeList = GetAllOptions();
					break;
				case RangeOptions.Past:
					model.DateRangeList = GetPastOptions();
					break;
				case RangeOptions.Custom:
					break;
			}

			if (model.RangeOptions == RangeOptions.Custom) return model;

			var defaultOption = model.DateRangeList.FirstOrDefault(e => e.DateRangeCode == model.DateRangeCode) ??
								model.DateRangeList.First();

			model.FromDate = defaultOption.FromDate;
			model.ToDate = defaultOption.ToDate;
			return model;
		}
		#endregion

		#region Private
		private static List<DateRangeOptionModel> GetAllOptions()
		{
			var r = new List<DateRangeOptionModel>
			{
				GetOption(DateRangeCode.Today),
				GetOption(DateRangeCode.ThisWeek),
				GetOption(DateRangeCode.ThisMonth),
				GetOption(DateRangeCode.ThisQuarter),
				GetOption(DateRangeCode.ThisYear),
				GetOption(DateRangeCode.YearToDate),

				GetOption(DateRangeCode.LastWeek),
				GetOption(DateRangeCode.LastMonth),
				GetOption(DateRangeCode.LastQuarter),
				GetOption(DateRangeCode.LastYear),

				GetOption(DateRangeCode.NextWeek),
				GetOption(DateRangeCode.NextMonth),
				GetOption(DateRangeCode.NextQuarter),
				GetOption(DateRangeCode.NextYear),

				GetOption(DateRangeCode.YearEnding),
				GetOption(DateRangeCode.Custom)
			};

			return r;
		}

		private static List<DateRangeOptionModel> GetPastOptions()
		{
			var r = new List<DateRangeOptionModel>
			{
				GetOption(DateRangeCode.Custom),
				GetOption(DateRangeCode.LastWeek),
				GetOption(DateRangeCode.LastMonth),
				GetOption(DateRangeCode.LastQuarter),
				GetOption(DateRangeCode.LastYear)
			};

			return r;
		}

		public static DateRangeOptionModel GetOption(DateRangeCode dateRangeCode)
		{
			// Calculate range days
			var start = DateTime.Today;
			var end = DateTime.Today;
			var day = DateTime.Today.DayOfWeek;
			var dayWeek = DateTime.Today.DayOfWeek;
			int dayDiff;

			switch (dateRangeCode)
			{
				case DateRangeCode.Custom:
					return new DateRangeOptionModel("Custom", DateRangeCode.Custom, DateTime.Today, DateTime.Today.AddDays(7));

				case DateRangeCode.Today:
					return new DateRangeOptionModel("Today", DateRangeCode.Today, DateTime.Today, DateTime.Today);

				case DateRangeCode.ThisWeek:
					int days = day - DayOfWeek.Monday;
					start = DateTime.Today.AddDays(-days);
					end = start.AddDays(6);
					return new DateRangeOptionModel("This Week", DateRangeCode.ThisWeek, start, end);

				case DateRangeCode.ThisMonth:
					start = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
					end = start.AddMonths(1).AddDays(-1);
					return new DateRangeOptionModel("This Month", DateRangeCode.ThisMonth, start, end);

				case DateRangeCode.ThisYear:
					start = new DateTime(DateTime.Today.Year, 1, 1);
					end = start.AddYears(1).AddDays(-1);
					return new DateRangeOptionModel("This Year", DateRangeCode.ThisYear, start, end);

				case DateRangeCode.YearToDate:
					start = new DateTime(DateTime.Today.Year, 1, 1);
					end = DateTime.Today;
					return new DateRangeOptionModel("Year-To-Date", DateRangeCode.YearToDate, start, end);

				case DateRangeCode.LastWeek:
					dayDiff = dayWeek - DayOfWeek.Sunday;
					start = DateTime.Today.AddDays(-dayDiff).AddDays(-7);
					end = start.AddDays(6);
					return new DateRangeOptionModel("Last Week", DateRangeCode.LastWeek, start, end);

				case DateRangeCode.LastMonth:
					start = DateTime.Today.AddMonths(-1);
					start = new DateTime(start.Year, start.Month, 1);
					end = start.AddMonths(1).AddDays(-1);
					return new DateRangeOptionModel("Last Month", DateRangeCode.LastMonth, start, end);

				case DateRangeCode.LastQuarter:
					return new DateRangeOptionModel("Last 3 Months", DateRangeCode.LastQuarter, DateTime.Today.AddMonths(-3), DateTime.Today);

				case DateRangeCode.LastYear:
					start = DateTime.Today.AddYears(-1);
					start = new DateTime(start.Year, 1, 1);
					end = start.AddYears(1).AddDays(-1);
					return new DateRangeOptionModel("Last Year", DateRangeCode.LastYear, start, end);

				case DateRangeCode.NextWeek:
					dayDiff = dayWeek - DayOfWeek.Sunday;
					start = DateTime.Today.AddDays(-dayDiff).AddDays(7);
					end = start.AddDays(6);
					return new DateRangeOptionModel("Next Week", DateRangeCode.NextWeek, start, end);

				case DateRangeCode.NextMonth:
					DateTime nextMonth = DateTime.Today.AddMonths(1);
					start = new DateTime(nextMonth.Year, nextMonth.Month, 1);
					end = start.AddMonths(1).AddDays(-1);
					return new DateRangeOptionModel("Next Month", DateRangeCode.NextMonth, start, end);

				case DateRangeCode.NextYear:
					start = new DateTime(DateTime.Today.AddYears(1).Year, 1, 1);
					end = start.AddYears(1).AddDays(-1);
					return new DateRangeOptionModel("Next Year", DateRangeCode.NextYear, start, end);

				case DateRangeCode.ThisQuarter:
					start = QuarterStart();
					end = QuarterEnd();
					return new DateRangeOptionModel("This Quarter", DateRangeCode.ThisQuarter, start, end);

				case DateRangeCode.NextQuarter:
					start = QuarterStart().AddMonths(3);
					end = QuarterEnd().AddMonths(3);
					return new DateRangeOptionModel("This Quarter", DateRangeCode.NextQuarter, start, end);

				case DateRangeCode.YearEnding:
					start = DateTime.Today.AddYears(-1);
					end = DateTime.Today;
					return new DateRangeOptionModel("Year Ending", DateRangeCode.YearEnding, start, end);
				default:
					return null;
			}
		}
		#endregion

		#region Helper Functions
		public static DateTime QuarterStart()
		{
			DateTime today = DateTime.Today;
			int year = today.Year;
			DateTime[] startOfQuarters = {
					new DateTime(year, 1, 1),
					new DateTime(year, 4, 1),
					new DateTime(year, 7, 1),
					new DateTime(year, 10, 1)
				};

			return startOfQuarters.Where(d => d.Subtract(today).Days <= 0).Last();
		}

		public static DateTime QuarterEnd()
		{
			DateTime today = DateTime.Today;
			int year = today.Year;
			DateTime[] endOfQuarters = {
					new DateTime(year, 3, 31),
					new DateTime(year, 6, 30),
					new DateTime(year, 9, 30),
					new DateTime(year, 12, 31)
				};

			return endOfQuarters.Where(d => d.Subtract(today).Days >= 0).First();
		}
		#endregion



	}
}