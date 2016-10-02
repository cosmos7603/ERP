using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AM.Services;
using Newtonsoft.Json.Linq;

namespace AM.WebSite.Shared.Controllers
{
	public class BaseController : Controller
	{

		//protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		//{
		//	Thread.CurrentThread.CurrentCulture =
		//		Thread.CurrentThread.CurrentUICulture =
		//			new CultureInfo(requestContext.HttpContext.Request["es-AR"]);

		//	base.Initialize(requestContext);
		//}

		#region Views Paths
		protected string ViewPath(string viewName)
		{
			// Add extension if missing
			if (!viewName.ToLower().EndsWith(".cshtml"))
				viewName += ".cshtml";

			// Already a full path?
			if (viewName.StartsWith("~"))
				return viewName;

			// No attribute?
			if (GetType().GetCustomAttributes(typeof(ViewsPath), false).Count() == 0)
				return viewName;

			// Get view path and return based on that
			var attribute = (ViewsPath)GetType().GetCustomAttributes(typeof(ViewsPath), false).First();

			return attribute.BasePath + "/" + viewName;
		}

		protected new ViewResult View()
		{
			return View(GetCurrentActionName());
		}

		protected new ViewResult View(string viewName)
		{
			return View(viewName, null);
		}

		protected new ViewResult View(object model)
		{
			return View(GetCurrentActionName(), model);
		}

		protected new ViewResult View(string viewName, object model)
		{
			return base.View(ViewPath(viewName), model);
		}

		protected new PartialViewResult PartialView()
		{
			return PartialView(GetCurrentActionName(), null);
		}

		protected new PartialViewResult PartialView(string viewName)
		{
			return PartialView(viewName, null);
		}

		protected new PartialViewResult PartialView(object model)
		{
			return PartialView(GetCurrentActionName(), model);
		}

		protected new PartialViewResult PartialView(string viewName, object model)
		{
			return base.PartialView(ViewPath(viewName), model);
		}
		#endregion

		#region JSon
		protected JsonResult GetJsonRedirect(string redirectUrl)
		{
			JSonResponse jSonResponse = new JSonResponse();
			jSonResponse.Redirect = redirectUrl;
			return GetJson(jSonResponse);
		}

		protected JsonResult GetJson(object data)
		{
			var result = Json(data);
			result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			return result;
		}

		protected JsonResult GetJsonResponse(ServiceResponse sr)
		{
			return GetJsonResponse(sr, "");
		}

		protected JsonResult GetJsonResponse(string successMessage)
		{
			return GetJsonResponse(null, successMessage);
		}

		protected JsonResult GetJsonResponse(ServiceResponse sr, string successMessage)
		{
			var jSonResponse = new JSonResponse(sr, successMessage);

			// Add warning only if it was not accepted
			if (sr != null && sr.Status && !String.IsNullOrEmpty(sr.WarningMessage) && !AcceptedWarning)
				jSonResponse.WarningMessage = sr.WarningMessage;

			return Json(jSonResponse);
		}

		protected string GetRequestJsonProperty(string propertyName)
		{
			Request.InputStream.Seek(0, SeekOrigin.Begin);
			var jsonData = new StreamReader(Request.InputStream).ReadToEnd();
			if (!jsonData.Contains(propertyName))
				return null;

			var jsonObject = JObject.Parse(jsonData);
			return jsonObject[propertyName].ToString();
		}
		#endregion

		#region Misc
		private string GetCurrentActionName()
		{
			return ControllerContext.RouteData.Values["action"].ToString();
		}

		public bool AcceptedWarning
		{
			get
			{
				var prop = GetRequestJsonProperty("acceptedWarning");
				return prop != null && bool.Parse(prop);
			}
		}

		#endregion
	}
}
