using System.Web.Mvc;
using AM.WebSite.Shared.Controllers;
using AM.WebSite.Controls.DataFileList.Models;

namespace AM.WebSite.Areas.Controls.DataFileList
{
	[Route("Controls/DataFileList/{action=index}")]
	[ViewsPath("~/Controls/DataFileList/Views")]
	public class DataFileListController : BaseController
	{
		#region Partial Views
		[HttpPost]
		public ActionResult FileList(DataFileListModel model)
		{
			return PartialView(model);
		}
		#endregion
	}
}
