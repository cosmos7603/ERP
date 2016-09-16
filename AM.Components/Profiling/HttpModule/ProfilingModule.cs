using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using Corpnet.Profiling;
using Corpnet.Profiling.Storage;
using System.Data;
using System.Collections.Generic;

namespace Corpnet.Profiling.HttpModule
{
	public sealed class ProfilingModule : IHttpModule
	{
		#region Private Statics
		private static volatile bool _applicationStarted = false;
		private static object _applicationStartLock = new object();
		#endregion

		#region Consts
		private const string SYSTEM_PARAM_NAME = "MiniProfiler";
		private const string SYSTEM_PARAM_GROUP = "General";
		#endregion

		#region Events
		public void Init(HttpApplication context)
		{
			if (!_applicationStarted)
            {
				lock (_applicationStartLock)
                {
					if (!_applicationStarted)
                    {
                        // this will run only once per application start
                        OnAppStart(context);

						// Mark as started so that it doesn't starts again
						_applicationStarted = true;
                    }
                }
            }

			context.BeginRequest += delegate
			{
				// Don't do nothing if profiling is not enabled
				if (!Settings.Running || !Settings.Enabled)
					return;

				// Filter requests like CSS, JS, etc
				if (!FilterRequest())
					return;

				// Add the response filter needed to caculate the response size
				HttpContext.Current.Response.Filter = new ResponseLengthStream(HttpContext.Current.Response.Filter);

				// We need to know when the headers has been sent, so that
				// we don't appen the client side tracking cookie
				HttpContext.Current.Cache["HeadersSent"] = false;
			};

			context.AcquireRequestState += delegate
			{
				// Don't do nothing if profiling is not enabled
				if (!Settings.Running || !Settings.Enabled)
					return;

				// Filter requests like CSS, JS, etc
				if (!FilterRequest())
					return;

				// Is it within tracked pages?
				if (Settings.ProfilingFilter.Rows.Count > 0)
				{
					bool profiled = false;

					foreach (DataRow dr in Settings.ProfilingFilter.Rows)
					{
						if (HttpContext.Current.Request.Url.PathAndQuery.Contains(dr["UrlString"].ToString()))
						{
							profiled = true;
							break;
						}
					}

					if (!profiled)
						return;
				}

				// Start profiler for this request
				MiniProfiler.Start();
			};

			context.EndRequest += delegate
			{
				// Don't do nothing if profiling is not enabled
				if (!Settings.Running || !Settings.Enabled)
					return;
	
				// Only stop profiler it there's one running
				if (MiniProfiler.Current != null)
				{
					MiniProfiler.Stop();

					// Set client side tracking cookie...
					// Only if there's a context with a valid response
					if (HttpContext.Current != null &&
						HttpContext.Current.Response != null &&
						// And it's not an AJAX request...
						!IsAjaxRequest(HttpContext.Current.Request) &&
						// And response headers has not been sent yet...
						!(bool)HttpContext.Current.Cache["HeadersSent"])
					{
						// Then we write the tracking cookie
						HttpCookie trackingCookie = new HttpCookie("CorpNetProfilerTracking");
						trackingCookie.Value = MiniProfiler.Current.RequestID;
						HttpContext.Current.Response.AppendCookie(trackingCookie);
					}
				}
			};

			context.Error += delegate
			{
				// Don't do nothing if profiling is not enabled
				if (!Settings.Running || !Settings.Enabled)
					return;

				// Register error info
				if (MiniProfiler.Current != null)
				{
					Exception ex = context.Server.GetLastError();

					if (ex != null)
					{
						string exceptionInfo = ex.ToString();

						if (ex.InnerException != null)
							exceptionInfo = ex.InnerException.ToString();

						MiniProfiler.Current.Exception = exceptionInfo;
					}
				}
			};

			context.PreSendRequestHeaders += delegate
			{
				HttpContext.Current.Cache["HeadersSent"] = true;
			};
		}

		private void OnAppStart(HttpApplication context)
		{
			// Initialize profiler
			MiniProfiler.Initialize();
		}

		public void Dispose()
		{
		}

		private static bool FilterRequest()
		{
			// Just in case, convert to lower case
			string filePath = HttpContext.Current.Request.CurrentExecutionFilePath;
			string extension = Path.GetExtension(filePath);

			// List of allowed extensions to profile
			List<string> allowedList = new List<string>(new string[] { "", ".aspx", ".ashx", ".asmx" });

			// Check and return
			return (allowedList.Contains(extension));
		}

		public static bool IsAjaxRequest(HttpRequest request)
		{
			if (request == null)
				return false;

			return (request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
		}
		#endregion
	}
}
