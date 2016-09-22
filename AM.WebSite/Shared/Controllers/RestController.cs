using System;
using System.IO;
using System.Web.Mvc;
using AM.Services;
using BLL.Abstract;
using BLL.Base;
using BLL.Concrete;
using Entities.POCOEntities;
using Newtonsoft.Json.Linq;

namespace AM.WebSite.Shared.Controllers
{
	public abstract class RestController<T> : Controller where T : EntityPOCO
	{
		protected RestController()
		{
			Manager = GetManager();
		}

		protected IManager<T> Manager { get; set; }

		private static IManager<T> GetManager()
		{
			return ManagerFactory.GetInstance().GetManagerFor<T>();
		}

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

			return base.Json(jSonResponse);
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
	}
}