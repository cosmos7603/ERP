using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AM.WebSite.Controls.MonthYearSelection.Models;
using AM.WebSite.MVC;
using AM.WebSite.Shared.Controllers;

namespace AM.WebSite.Controls.MonthYearSelection
{
	[Route("Controls/MonthYearSelection/{action=index}")]
    [ViewsPath("~/Controls/MonthYearSelection/Views")]
    public class MonthYearSelectionController : BaseController
    {
        #region Partial View
        [HttpPost]
        public ActionResult MonthYearSelection(MonthYearSelectionModel requestModel)
        {
            var model = GetMonthYearSelectionModel(requestModel);

            return PartialView(model);
        }
		#endregion

		#region Models
		public static MonthYearSelectionModel GetMonthYearSelectionModel(MonthYearSelectionModel model)
		{
			var yearList = new List<int>();

			switch (model.MonthYearOptions)
			{
				case MonthYearSelectionOptions.All:
					for (int y = model.MinYear; y <= model.MaxYear; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.Past:
					for (int y = model.MinYear; y <= DateTime.Today.Year; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.NextYear:
					for (int y = model.MinYear; y <= DateTime.Today.Year + 1; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.LastYear:
					for (int y = DateTime.Today.Year - 1; y <= DateTime.Today.Year; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.Last2Years:
					for (int y = DateTime.Today.Year - 2; y <= DateTime.Today.Year; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.Last5Years:
					for (int y = DateTime.Today.Year - 5; y <= DateTime.Today.Year; y++)
						yearList.Add(y);
					break;
				case MonthYearSelectionOptions.Span10Years:
					for (int y = DateTime.Today.Year - 5; y <= DateTime.Today.Year + 5; y++)
						yearList.Add(y);
					break;
			}

			// Initial values
			model.Year = DateTime.Today.Year;
			model.Month = DateTime.Today.Month;

			// Month list
			var monthList = Enumerable.Range(1, 12).Select(i => new { MonthNumber = i, MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) }).ToList();

			model.YearSelectList = yearList.ToSelectList(x => x.ToString(), x => x.ToString(), model.Year.ToString());
			model.MonthSelectList = monthList.ToSelectList(x => x.MonthName.ToString(), x => x.MonthNumber.ToString(), model.Month.ToString());

			return model;
        }
		#endregion
	}
}