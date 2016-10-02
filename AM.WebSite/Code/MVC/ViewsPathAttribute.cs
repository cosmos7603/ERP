using System;

namespace AM.WebSite
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewsPath : Attribute
	{
		public string BasePath { get; set; }

		public ViewsPath(string basePath)
		{
			BasePath = basePath;
		}
	}
}