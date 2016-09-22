using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AM.WebSite
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class ViewsPath : Attribute
	{
		public string BasePath { get; set; }

		public ViewsPath(string basePath)
		{
			BasePath = basePath;
		}
	}
}