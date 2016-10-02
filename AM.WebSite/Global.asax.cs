using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AM.Services;
using AM.Services.Models;
using AM.WebSite.Areas.Shared.Error;

namespace AM.WebSite
{
	public class MvcApplication : HttpApplication
	{

	

		protected void Application_Start()
		{
			// Configure Mini Profiler
			//ProfilingConfig.RegisterProfiling();

			//// Service Config
			//ServiceConfig.RegisterConfig(this);
			Config.Database.ConnectionString = AppInfo.WebConfig.ConnectionString;

			// Application start
			//	LogService.Info(EventCode.APP_START, "Application starting.");

			AreaRegistration.RegisterAllAreas();

			//LogService.Info(EventCode.APP_START, "Registering.");

			// Startup Configs
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			BinderConfig.RegisterBinders();
			//EncryptionConfig.RegisterEncrytion();

			//LogService.Info(EventCode.APP_START, "Registering completed.");

			// Path for the email templates
			//EmailingService.EmailTemplatePath = Server.MapPath("~/Content/Emails");
			//EmailingService.EmailingEnabled = true;
			//EmailingService.StartQueueManager();

			//LogService.Info(EventCode.APP_START, "Application starting completed.");
		}

		protected void Application_End()
		{
		//	LogService.Info(EventCode.APP_END, "Application end.");
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			AppInfo.DeserializeActiveUser();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError();
			HttpContextWrapper contextWrapper = new HttpContextWrapper(Context);
			RouteData routeData = new RouteData();

			// Clear error status
			Server.ClearError();

			// Get the most inner exception
			while (exception.InnerException != null) exception = exception.InnerException;

			// Render error view
			routeData.Values.Add("controller", "Error");
			routeData.Values.Add("action", "Exception");
			routeData.Values.Add("exception", exception);

			IController controller = new ErrorController();
			RequestContext requestContext = new RequestContext(contextWrapper, routeData);

			try
			{
				controller.Execute(requestContext);
			}
			catch (Exception ex)
			{
				// The error is too high on the hirarchy, and the MVC context can't be instanced
				Response.Write(ex.ToString());
			}
		}

		protected void Application_BeginRequest()
		{
			AppInfo.SetDateFormat();
		}

		protected void Application_EndRequest()
		{
			ServiceBase.DisposeContext();
		}
	}
}