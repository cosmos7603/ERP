﻿using AM.WebSite.Controls.Tabs.Models;
using AM.WebSite.Controls.WaitingSpinner.Consts;
using AM.WebSite.Controls.WaitingSpinner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region Tabs
		public static MvcHtmlString PSWaitingSpinner(this HtmlHelper htmlHelper, string id)
		{
			return htmlHelper.PSWaitingSpinner(id, "sm");
		}

		public static MvcHtmlString PSWaitingSpinner(this HtmlHelper htmlHelper, string id, string size)
		{
			var model = new WaitingSpinnerModel { ID = id, Size = size };

			return htmlHelper.PSWaitingSpinner(model);
		}

		public static MvcHtmlString PSWaitingSpinner(this HtmlHelper htmlHelper, WaitingSpinnerModel model)
		{
			return htmlHelper.Partial("~/Controls/WaitingSpinner/Views/WaitingSpinner.cshtml", model);
		}
		#endregion
	}
}