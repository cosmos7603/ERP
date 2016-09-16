using AM.WebSite.Code.MVC;
using System.Web;
using System.Web.Mvc;

namespace AM.WebSite
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new ServiceIdentityFilter());
		}
	}
}