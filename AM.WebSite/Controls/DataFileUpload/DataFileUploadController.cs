using AM.WebSite.Controls.DataFileUpload.Models;
using AM.WebSite.Shared.Controllers;
using System.Web.Mvc;

namespace AM.WebSite.Controls.DataFileUpload
{
	[Route("Controls/DataFileUpload/{action=index}")]
    [ViewsPath("~/Controls/DataFileUpload/Views")]
	public class DataFileUploadController : BaseController
	{
		#region Partial Views
		[HttpPost]
		public ActionResult DataFileUpload(DataFileUploadModel model)
		{
			return PartialView(model);
		}
		#endregion
	}
}