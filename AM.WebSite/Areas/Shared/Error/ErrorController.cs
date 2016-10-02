using System;
using System.Net;
using System.Web.Mvc;
using AM.Services;
using AM.Services.Support;
using AM.WebSite.Areas.Shared.Error.Models;
using AM.WebSite.Shared.Controllers;

namespace AM.WebSite.Areas.Shared.Error
{
	[ViewsPath("~/Areas/Shared/Error/Views")]
	public class ErrorController : BaseController
	{
		#region Exception
		public ActionResult Exception(Exception exception)
		{
			// Return 500
			Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			// Log error
			var eventLogId = LogService.Error(EventCode.APP, exception);

			// Build error page
			var model = new ExceptionModel();

			model.TrackingCode = eventLogId.ToString();
			model.Message = exception.Message;
			model.StackTrace = exception.StackTrace;

			return View(model);
		}
		#endregion
	}
}
