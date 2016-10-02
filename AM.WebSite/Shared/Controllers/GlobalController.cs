using System.Reflection;
using AM.Services;

namespace AM.WebSite.Shared.Controllers
{
	public class GlobalController : BaseController
	{
		#region URLs
		public string URLs()
		{
			return (string)CacheService.GetCachedItem("URLs", () => UrlGenerator.GenerateUrls(Assembly.GetExecutingAssembly()));
		}
		#endregion
	}
}
