using AM.DAL;
using AM.Services.Models;
using AM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace AM.Services.Support
{
	#region Enums
	public enum EventCode
	{
		APP,
		APP_START,
		APP_END,
		CONFIG,
		EMAILING,
		REPORTING,
		IMPORT,
		JOB,
		API,
		SUPPORT,
		INFO
	}

	public enum LogLevel
	{
		ERROR,
		WARN,
		INFO,
		DEBUG
	}
	#endregion

	public class LogService : ServiceBase
	{
		#region Properties
		public static bool ConsoleOutput { get; set; }
		#endregion

		#region Query
		public static EventLog GetEventLog(int eventLogId)
		{
			return DB
				.EventLog
				.AsNoTracking()
				.Where(x => x.EventLogId == eventLogId)
				.FirstOrDefault();
		}

		public static List<EventLog> GetEventLogTrail(int eventLogId)
		{
			var e = GetEventLog(eventLogId);

			return DB
				.EventLog
				.AsNoTracking()
				.Where(x => x.Context == e.Context)
				.OrderBy(x => x.EventDate)
				.ToList();
		}
		#endregion

		#region Log Error
		public static int? Error(EventCode eventCode, string description)
		{
			return Log(eventCode, description, LogLevel.ERROR);
		}

		public static int? Error(EventCode eventCode, Exception ex)
		{
			return Log(eventCode, ex.Message, ex, ex.Data, LogLevel.ERROR);
		}

		public static int? Error(EventCode eventCode, string description, Exception exception)
		{
			return Log(eventCode, description, exception, null, LogLevel.ERROR);
		}

		public static int? Error(EventCode eventCode, string description, object payLoad)
		{
			return Log(eventCode, description, null, payLoad, LogLevel.ERROR);
		}

		public static int? Error(EventCode eventCode, string description, Exception exception, object payLoad)
		{
			return Log(eventCode, description, exception, payLoad, LogLevel.ERROR);
		}
		#endregion

		#region Log Warn
		public static int? Warn(EventCode eventCode, string description)
		{
			return Log(eventCode, description, LogLevel.WARN);
		}

		public static int? Warn(EventCode eventCode, string description, object payload)
		{
			return Log(eventCode, description, null, payload, LogLevel.WARN);
		}

		public static int? Warn(EventCode eventCode, Exception exception)
		{
			return Log(eventCode, exception.Message, exception, null, LogLevel.WARN);
		}

		public static int? Warn(EventCode eventCode, Exception exception, object payload)
		{
			return Log(eventCode, exception.Message, exception, payload, LogLevel.WARN);
		}
		#endregion

		#region Log Info
		public static int? Info(EventCode eventCode, string description)
		{
			return Log(eventCode, description, LogLevel.INFO);
		}

		public static int? Info(EventCode eventCode, string description, object payload)
		{
			return Log(eventCode, description, null, payload, LogLevel.INFO);
		}
		#endregion

		#region Log Debug
		public static int? Debug(EventCode eventCode, string description)
		{
			return Log(eventCode, description, LogLevel.DEBUG);
		}

		public static int? Debug(EventCode eventCode, string description, object payload)
		{
			return Log(eventCode, description, null, payload, LogLevel.DEBUG);
		}
		#endregion

		#region Log
		public static int? Log(EventCode eventCode, string description, LogLevel logLevel)
		{
			return Log(eventCode, description, null, null, logLevel);
		}

		public static int? Log(EventCode eventCode, string description, Exception exception, object payload, LogLevel logLevel)
		{
			if (!Config.Support.EnableLogging)
				return null;

			var systemLogLevel = Config.Support.LogLevel;

			if (systemLogLevel == LogLevel.WARN.ToString() && (logLevel != LogLevel.ERROR || logLevel != LogLevel.WARN))
				return null;

			if (systemLogLevel == LogLevel.INFO.ToString() && logLevel == LogLevel.DEBUG)
				return null;

			var db = new DB();

			// Save in DB
			EventLog eventLog = new EventLog();

			eventLog.EventDate = DateTime.Now;
			eventLog.Login = Identity.AuditLogin;
			eventLog.LogLevel = logLevel.ToString();
			eventLog.EventCode = eventCode.ToString();
			eventLog.Description = description;
			eventLog.Exception = SerializeException(exception).NullEmptyString();
			eventLog.Context = GetContext();

			// Log payload
			if (payload != null)
			{
				var t = payload.GetType();

				if (t.IsPrimitive || t == typeof(Decimal) || t == typeof(String))
					eventLog.Payload = payload.ToString();
				else
					eventLog.Payload = payload.ToJson();
			}

			db.EventLog.Add(eventLog);
			db.SaveChanges();

			// Output to console as well?
			if (ConsoleOutput)
				Console.WriteLine(eventCode + " - " + description);

			// Return final message
			return eventLog.EventLogId;
		}

		private static string SerializeException(Exception ex)
		{
			var e = ex;

			if (e == null)
				return "";

			for (int i = 0; i < 5; i++)
			{
				if (e.InnerException != null)
					e = e.InnerException;
			}

			var r = new
			{
				Message = e.Message,
				Source = e.Source,
				StackTrace = e.StackTrace
			};

			return r.ToJson();
		}
		#endregion

		#region Context
		public static string GetContext()
		{
			// Find DB on this thread's bag
			var context = CallContext.GetData("LoggingContext") as string;

			// If it doesn't exists, create it
			if (context == null)
			{
				context = Guid.NewGuid().ToString();

				// Save the context on the thread's bag
				CallContext.SetData("LoggingContext", context);
			}

			return context;
		}
		#endregion
	}
}
