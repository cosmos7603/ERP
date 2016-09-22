using AM.WebSite.Areas.Shared.Error.Models;
using System.Web;
using System.Web.Mvc;

namespace AM.WebSite.Code.Authorization
{
	public class UnauthorizedResults
	{
		#region Methods
		public static ActionResult CreateUnauthorizedResult(AuthorizationContext filterContext)
		{
			UnauthorizedModel unauthorizedModel = new UnauthorizedModel();
			unauthorizedModel.ControllerName = (string)filterContext.RouteData.Values["controller"];
			unauthorizedModel.ActionName = (string)filterContext.RouteData.Values["action"];
			unauthorizedModel.Message = "You do not have sufficient privileges for this operation.";

			// custom logic to determine proper view here - i'm just hardcoding it
			string viewName = "~/Areas/Shared/Error/Unauthorized/Views/Unauthorized.cshtml";

			return new ViewResult
			{
				ViewName = viewName,
				ViewData = new ViewDataDictionary<UnauthorizedModel>(unauthorizedModel)
			};
		}

		public static ActionResult CreateSessionExpiredResult(AuthorizationContext filterContext)
		{
			HttpContext httpContext = HttpContext.Current;
			var appPath = httpContext.Request.ApplicationPath;
			if (appPath[appPath.Length - 1] == '/')
				appPath = appPath.Remove(appPath.Length - 1);

			SessionExpiredModel sessionExpiredModel = new SessionExpiredModel();
			sessionExpiredModel.LoginPage = "http://" + httpContext.Request.Url.Host + appPath + "/Accounts/Login";

			// Add header so that we can detec this on ajaxSuccess
			httpContext.Response.AddHeader("X-LOGIN-PAGE", sessionExpiredModel.LoginPage);

			// Custom logic to determine proper view here - i'm just hardcoding it
			string viewName = "~/Areas/Shared/Error/SessionExpired/Views/SessionExpired.cshtml";

			return new ViewResult
			{
				ViewName = viewName,
				ViewData = new ViewDataDictionary<SessionExpiredModel>(sessionExpiredModel)
			};
		}
		#endregion
	}
}