using System;
using System.Web;
using CultureInfo = System.Globalization.CultureInfo;
using Encoding = System.Text.Encoding;

namespace Corpnet.Profiling.HttpModule
{
	public class PageFactory : IHttpHandlerFactory
	{
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
            // The request resource is determined by the looking up the
            // value of the PATH_INFO server variable.
            string resource = context.Request.PathInfo.Length == 0 ? string.Empty :
                context.Request.PathInfo.Substring(1);

			switch (resource.ToLower(CultureInfo.InvariantCulture))
			{
				case "stylesheet":
					return new ManifestResourceHandler("ProfilingConsole.css", "text/css", Encoding.GetEncoding("Windows-1252"));
				default:
					{
						if (resource.Length == 0)
						{
							return new ConsolePage();
						}
						else
						{
							throw new HttpException(404, "Resource not found.");
						}
					}
			}
		}

		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
