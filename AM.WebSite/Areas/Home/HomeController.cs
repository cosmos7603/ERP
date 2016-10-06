﻿using System.Web.Mvc;
using AM.WebSite.Shared.Controllers;

namespace AM.WebSite.Areas.Home
{
	//[Authorize]
	[ViewsPath("~/Areas/Home/Views")]
	[Route("Home/{action=index}")]
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
