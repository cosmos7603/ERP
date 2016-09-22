using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AM.Utils
{
	public static class HttpHelper
	{
		public static string HttpPost(string uri, string parameters)
		{
			// parameters: name1=value1&name2=value2	
			WebRequest webRequest = WebRequest.Create(uri);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";

			byte[] bytes = Encoding.ASCII.GetBytes(parameters);
			Stream os = null;

			try
			{
				webRequest.ContentLength = bytes.Length;
				os = webRequest.GetRequestStream();
				os.Write(bytes, 0, bytes.Length);
			}
			catch (WebException ex)
			{
				throw (ex);
			}
			finally
			{
				if (os != null)
				{
					os.Close();
				}
			}

			try
			{
				WebResponse webResponse = webRequest.GetResponse();

				if (webResponse == null)
					return null;

				StreamReader sr = new StreamReader(webResponse.GetResponseStream());

				return sr.ReadToEnd().Trim();
			}
			catch (WebException ex)
			{
				throw (ex);
			}
		}

        public static string SafeUrlEncode(string url)
        {
            return (HttpUtility.UrlEncode(url));
        }

		public static string SafeUrlDecode(string url)
		{
			return (HttpUtility.UrlDecode(url.Replace("+", "%2B")));
		}

        public static string GetClientIPAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
	}
}
