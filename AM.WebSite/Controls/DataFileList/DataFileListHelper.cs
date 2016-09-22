using AM.WebSite.Controls.Captcha;
using AM.WebSite.Controls.Captcha.Models;
using AM.WebSite.Controls.DataFileList.Models;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AM.WebSite.HtmlHelpers
{
	public static partial class HtmlHelperExtensions
	{
		#region DataFileList
		public static MvcHtmlString PSDataFileList(this HtmlHelper htmlHelper, DataFileListModel model)
		{
			return htmlHelper.Partial("~/Controls/DataFileList/Views/DataFileList.cshtml", model);
		}
		#endregion
	}
}