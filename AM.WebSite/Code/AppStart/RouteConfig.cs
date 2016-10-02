using System.Web.Mvc;
using System.Web.Routing;

namespace AM.WebSite
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			// Ignore axd resources
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// Attribute Routing
			routes.MapMvcAttributeRoutes();

			// Default routes
			routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new[] { "AM.WebSite.Controllers" }
			);
		}
	}
}