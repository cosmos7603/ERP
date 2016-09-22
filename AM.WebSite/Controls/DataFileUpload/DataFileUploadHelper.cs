using AM.WebSite.Controls.Captcha;
using AM.WebSite.Controls.Captcha.Models;
using AM.WebSite.Controls.DataFileUpload.Models;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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