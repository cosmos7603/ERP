using System;
using System.Collections.Generic;

namespace AM.WebSite.Controls.DateRange.Models
{
	#region Enums
	public enum DateRangeCode
	{
		Today,
		ThisWeek,
		ThisMonth,
		ThisQuarter,
		ThisYear,
		YearToDate,
		LastWeek,
		LastMonth,
		LastQuarter,
		LastYear,
		NextWeek,
		NextMonth,
		NextQuarter,
		NextYear,
		YearEnding,
		Custom
	}

	public enum RangeOptions
	{
		All,
		Past,
		Custom
	}
	#endregion

	public class DateRangeModel
	{
		public RangeOptions RangeOptions { get; set; }
		public string Title = "Date Range";
		public List<DateRangeOptionModel> DateRangeList { get; set; }
		public DateRangeCode DateRangeCode { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string ID { get; set; }

		public DateRangeModel()
		{
			DateRangeCode = DateRangeCode.ThisWeek;
			Title = "Dates";
			RangeOptions = RangeOptions.All;
		}
	
		public DateRangeModel(RangeOptions rangeOptions, DateRangeCode defaultRangeCode)
		{
			DateRangeCode = defaultRangeCode;
			RangeOptions = rangeOptions;
		}

		public DateRangeModel(RangeOptions rangeOptions, DateRangeCode defaultRangeCode, string title)
		{
			DateRangeCode = defaultRangeCode;
			RangeOptions = rangeOptions;
			Title = title;
        }
	}

	public class DateRangeOptionModel
	{
		public DateRangeCode DateRangeCode { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }
		public string Title { get; set; }

		public DateRangeOptionModel(string title, DateRangeCode dateRangeCode, DateTime? fromDate, DateTime? toDate)
		{
			Title = title;
			DateRangeCode = dateRangeCode;

			if (fromDate.HasValue)
				FromDate = fromDate.Value;
			else
				FromDate = null;

			if (toDate.HasValue)
				ToDate = toDate.Value;
			else
				ToDate = null;
		}
	}
}