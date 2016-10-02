using System.Web.Mvc;
using Corpnet.Profiling.EF6;
using Corpnet.Profiling.MVC;

namespace AM.WebSite
{
	public class ProfilingConfig
	{
		public static void RegisterProfiling()
		{
			// Initialize EF profiling
			EFProfiling.Initialize();

			// Initialize MVC profiling
			GlobalFilters.Filters.Add(new ProfilingActionFilter());
		}
	}
}