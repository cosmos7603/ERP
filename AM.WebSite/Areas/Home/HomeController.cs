using System.Web.Mvc;
using AM.WebSite.Shared.Controllers;
using AM.Services;

namespace AM.WebSite.Areas.Home
{
	//[Authorize]
	[ViewsPath("~/Areas/Home/Views")]
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			//if (!User.Identity.IsAuthenticated || AppInfo.SessionInfo == null)
			//	return RedirectToAction("Index", "Login");

			//return RedirectToAction("Index", "Dashboard");
			return View();
		}
	}
}
