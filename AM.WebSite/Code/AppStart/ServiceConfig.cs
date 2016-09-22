using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AM.Services;
using AM.Services.Models;
using AM.Services.Support;

namespace AM.WebSite
{
	public class ServiceConfig
	{
		public static void RegisterConfig(HttpApplication httpApplication)
		{
			// Connection string (we need to have this first, to start logging)
			Config.Database.ConnectionString = AppInfo.WebConfig.ConnectionString;

			// Paths
			Config.Paths.Application = httpApplication.Server.MapPath("~");
			Config.Paths.Temp = httpApplication.Server.MapPath("~/Temp");
			Config.Paths.EmailTemplates = httpApplication.Server.MapPath("~/Content/Emails");

			// Load all other configs from database
			ConfigService.LoadConfig();

			// Initialize temp folder
			FileService.InitializeTempFolder();

			// Set Config refresh task (Disabled for now, it was clogging the event log)
			// ConfigService.SetupRefreshConfigTask();

			// Path for the email templates
			EmailingService.EmailTemplatePath = Config.Paths.EmailTemplates;
			EmailingService.EmailingEnabled = true;
			EmailingService.StartQueueManager();
		}
	}
}