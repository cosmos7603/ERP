using System;
using System.Web.Mvc;

namespace AM.WebSite.Controls.MonthYearSelection.Models
{
	#region Enums
	public enum MonthYearSelectionOptions
	{
		All,
		Past,
		NextYear,
		LastYear,
		Last5Years,
        Last2Years,
		Span10Years
	}
	#endregion

	public class MonthYearSelectionModel
	{
		public MonthYearSelectionOptions MonthYearOptions { get; set; }
		public string Title = "Month / Year";
		public int MinYear { get; set; }
		public int MaxYear { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public string ID { get; set; }
		public SelectList YearSelectList { get; set; }
		public SelectList MonthSelectList { get; set; }

		public MonthYearSelectionModel()
		{
			MinYear = 1900;
			MaxYear = 2099;
			Year = DateTime.Today.Year;
			Month = DateTime.Today.Month;
			Title = "Month / Year";
			MonthYearOptions = MonthYearSelectionOptions.All;
		}
	
		public MonthYearSelectionModel(MonthYearSelectionOptions monthYearOptions, int month, int year, string title)
		{
			MonthYearOptions = monthYearOptions;
			Month = month;
			Year = year;
			Title = title;
        }
	}
}