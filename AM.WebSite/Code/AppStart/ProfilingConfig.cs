using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AM.Services;
using System.Web.Mvc;

namespace AM.WebSite
{
	public class ProfilingConfig
	{
		public static void RegisterProfiling()
		{
			// Initialize EF profiling
			Corpnet.Profiling.EF6.EFProfiling.Initialize();

			// Initialize MVC profiling
			GlobalFilters.Filters.Add(new Corpnet.Profiling.MVC.ProfilingActionFilter());
		}
	}
}