using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using Corpnet.Profiling.Extensions;

namespace Corpnet.Profiling
{
    /// <summary>
    /// HttpContext based profiler provider.  This is the default provider to use in a web context.
    /// The current profiler is associated with a HttpContext.Current ensuring that profilers are 
    /// specific to a individual HttpRequest.
    /// </summary>
    public partial class WebRequestProfilerProvider : BaseProfilerProvider
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WebRequestProfilerProvider"/> class. 
        /// Public constructor.  This also registers any UI routes needed to display results
        /// </summary>
        public WebRequestProfilerProvider()
        {
        }

        /// <summary>
        /// Starts a new MiniProfiler and associates it with the current <see cref="HttpContext.Current"/>.
        /// </summary>
        public override MiniProfiler Start()
        {
            var context = HttpContext.Current;
            if (context == null) return null;

            var url = context.Request.Url;
            var path = context.Request.AppRelativeCurrentExecutionFilePath.Substring(1).ToUpperInvariant();
			var name = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1].ToString();
            
			var result = new MiniProfiler(name);
            
			Current = result;

            SetProfilerActive(result);

			result.Action = HttpContext.Current.Request.HttpMethod.ToUpper();
			result.Url = url.OriginalString;
			result.Event = GetEvent();
			result.Referer = context.Request.ServerVariables["HTTP_REFERER"];
            result.ClientIP = context.Request.ServerVariables["REMOTE_ADDR"];
			result.ClientAgent = context.Request.ServerVariables["HTTP_USER_AGENT"];
			result.RequestID = Guid.NewGuid().ToString().ToUpper();
			result.RequestSize = HttpContext.Current.Request.TotalBytes;
			result.ActiveUser = "";
			result.Exception = "";

			// Read cookie with client timings
			if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				HttpCookie trackingCookie = HttpContext.Current.Request.Cookies["CorpNetProfilerTracking"];

				if (trackingCookie != null)
				{
					string[] parts = trackingCookie.Value.Split('|');

					// Malformed?
					if (parts.Length == 9)
					{
						string requestID = parts[0].ToString();
						Guid guid;

						// Only save if the requestID is a valid GUID
						if (Guid.TryParse(requestID, out guid))
						{
							result.ClientRequestID = requestID;
							result.ClientTotalDuration = parts[1].ToDecimal();
							result.ClientRedirectDuration = parts[2].ToDecimal();
							result.ClientDnsDuration = parts[3].ToDecimal();
							result.ClientConnectionDuration = parts[4].ToDecimal();
							result.ClientRequestDuration = parts[5].ToDecimal();
							result.ClientResponseDuration = parts[6].ToDecimal();
							result.ClientDomDuration = parts[7].ToDecimal();
							result.ClientLoadDuration = parts[8].ToDecimal();
						}
					}
				}
			}

			// Add session ID
			if (HttpContext.Current != null && HttpContext.Current.Session != null)
				result.SessionID = HttpContext.Current.Session.SessionID;

			// Add header id response header (for tracing on IIS)
			context.Response.AddHeader("Request-ID", result.RequestID);

			// Obtain activeuser from session
			if (!String.IsNullOrEmpty(Settings.ActiveUserLocator))
				result.ActiveUser = GetActiveUser(Settings.ActiveUserLocator);

			return result;
        }

		private string GetEvent()
		{
			string eventTarget = HttpContext.Current.Request.Form["__EVENTTARGET"];
			string eventArgument = HttpContext.Current.Request.Form["__EVENTARGUMENT"];

			if (String.IsNullOrEmpty(eventTarget))
				return "";

			if (!String.IsNullOrEmpty(eventArgument))
				return eventTarget + "/" + eventArgument;
			else
				return eventTarget;
		}

		private string GetActiveUser(string key)
		{
			if (key == "USER_IDENTITY")
			{
				if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
					return HttpContext.Current.User.Identity.Name;
				else
					return "";
			}

			if (key.IndexOf(".") != -1)
			{
				string[] parts = key.Split('.');
				
				string objectName = parts[0];
				string propertyName = parts[1];

				if (HttpContext.Current.Session == null)
					return "";

				object activeUser = HttpContext.Current.Session[objectName];

				if (activeUser == null)
					return "";

				PropertyInfo pi = activeUser.GetType().GetProperty(propertyName);

				if (pi == null)
					return "";

				return pi.GetValue(activeUser, null).ToString();
			}
			else
			{
				object activeUser = HttpContext.Current.Session[key];

				if (activeUser == null)
					return "";

				return activeUser.ToString();
			}
		}

        /// <summary>
        /// Ends the current profiling session, if one exists.
        /// </summary>
        /// <param name="discardResults">
        /// When true, clears the <see cref="MiniProfiler.Current"/> for this HttpContext, allowing profiling to 
        /// be prematurely stopped and discarded. Useful for when a specific route does not need to be profiled.
        /// </param>
        public override void Stop(bool discardResults)
        {
            var context = HttpContext.Current;
            if (context == null)
                return;

            var current = Current;
            if (current == null)
                return;

            // stop our timings - when this is false, we've already called .Stop before on this session
            if (!StopProfiler(current))
                return;

			//DateTime startDate = DateTime.Now;

            if (discardResults)
            {
                Current = null;
                return;
            }

            var request = context.Request;
            var response = context.Response;

            // set the profiler name to Controller/Action or /url
            EnsureName(current, request);

            // save the profiler
            SaveProfiler(current);

			// Uncomment to measure time spent on saving timings to the memory queue
			//DateTime endDate = DateTime.Now;
			//double totalMs = endDate.Subtract(startDate).TotalMilliseconds;
			//if (totalMs > 0)
			//	File.AppendAllText(context.Server.MapPath("/CruiseWeb/delays.txt"), DateTime.Now.ToLongTimeString() + " - " + totalMs.ToString() + Environment.NewLine);
		}
		
		/// <summary>
        /// Makes sure 'profiler' has a Name, pulling it from route data or url.
        /// </summary>
        private static void EnsureName(MiniProfiler profiler, HttpRequest request)
        {
            // also set the profiler name to Controller/Action or /url
            if (string.IsNullOrEmpty(profiler.Url))
            {
				if (string.IsNullOrEmpty(profiler.Url))
				{
					profiler.Url = request.Url.AbsolutePath ?? string.Empty;
					if (profiler.Url.Length > 50)
						profiler.Url = profiler.Url.Remove(50);
				}
            }
        }

        /// <summary>
        /// Returns the current profiler
        /// </summary>
        public override MiniProfiler GetCurrentProfiler()
        {
            return Current;
        }


        private const string CacheKey = ":mini-profiler:";

        /// <summary>
        /// Gets the currently running MiniProfiler for the current HttpContext; null if no MiniProfiler was <see cref="Start"/>ed.
        /// </summary>
        private MiniProfiler Current
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null) return null;

                return context.Items[CacheKey] as MiniProfiler;
            }
            set
            {
                var context = HttpContext.Current;
                if (context == null) return;

                context.Items[CacheKey] = value;
            }
        }
    }
}
