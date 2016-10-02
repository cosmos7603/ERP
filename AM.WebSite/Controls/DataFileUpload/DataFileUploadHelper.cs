using System.Web.Mvc;
using System.Web.Mvc.Html;
using AM.WebSite.Controls.DataFileUpload.Models;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region DataFileUpload
		public static MvcHtmlString PSDataFileUpload(this HtmlHelper htmlHelper, string id)
		{
			return PSDataFileUpload(htmlHelper, new DataFileUploadModel
			{
				ID = id
			});
		}

		public static MvcHtmlString PSDataFileUpload(this HtmlHelper htmlHelper, DataFileUploadModel model)
		{
			return htmlHelper.Partial("~/Controls/DataFileUpload/Views/DataFileUpload.cshtml", model);
		}
		#endregion
	}
}