using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Timers;
using System.Web;
using Corpnet.Profiling.Config;
using Corpnet.Profiling.Extensions;
using Corpnet.Profiling.Interfaces;
using Corpnet.Profiling.Log;
using Corpnet.Profiling.Schedule;

namespace Corpnet.Profiling
{
	[Serializable]
    public partial class MiniProfiler
	{
		#region Consts
		private const int ONE_MINUTE = 1000 * 60;
		#endregion

		#region Members
		private readonly IStopwatch _sw;
		private static Timer _resumeStorageTimer;
		#endregion

		#region Constructors
		public MiniProfiler(string name)
        {
            Id = Guid.NewGuid();
            MachineName = Environment.MachineName;
            Started = DateTime.Now;
			Name = name;
			Timings = new List<Timing>();

            _sw = Settings.StopwatchProvider();
        }
		#endregion

		#region Properties
		public static bool StorageSuspended { get; set; }
		public Guid Id { get; set; }
		public string Referer { get; set; }
		public string Action { get; set; }
		public string Event { get; set; }
		public string ActiveUser { get; set; }
		public string ClientIP { get; set; }
		public string ClientAgent { get; set; }
		public string RequestID { get; set; }
		public bool IsActive { get; set; }
		public int DbProfilingId { get; set; }
		public long RequestSize { get; set; }
		public long ResponseSize { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
        public DateTime Started { get; set; }
        public decimal DurationMilliseconds { get; set; }
        public string MachineName { get; set; }
		public string Exception { get; set; }
		public string SessionID { get; set; }
		public string ClientRequestID { get; set; }
		public decimal ClientTotalDuration { get; set; }
		public decimal ClientRedirectDuration { get; set; }
		public decimal ClientDnsDuration { get; set; }
		public decimal ClientConnectionDuration { get; set; }
		public decimal ClientRequestDuration { get; set; }
		public decimal ClientResponseDuration { get; set; }
		public decimal ClientDomDuration { get; set; }
		public decimal ClientLoadDuration { get; set; }

		public List<Timing> Timings { get; set; }

        internal long ElapsedTicks
        {
            get { return _sw.ElapsedTicks; }
        }

        internal IStopwatch Stopwatch
        {
            get { return _sw; }
        }

		public static MiniProfiler Current
        {
            get
            {
                Settings.EnsureProfilerProvider();
                return Settings.ProfilerProvider.GetCurrentProfiler();
            }
        }
		#endregion

		#region Events
		private static void ResumeStorageTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Logger.Log("Resume storage timer fired. Will try to resume storage...");

			ResumeStorage();
		}
		#endregion

		#region Methods
		public static void Initialize()
		{
			// This can't throw any exception, or the whole web app won't start
			// So if something happens, we just left MP disabled and that's it
			try
			{
				// Log
				Logger.Log("Application started.");

				// For normal usage, this will return a System.Diagnostics.Stopwatch to collect times - unit tests can explicitly set how much time elapses
				Settings.StopwatchProvider = StopwatchWrapper.StartNew;

				// Read settings from database
				Settings.LoadProfilingConfig();

				// Initialize
				MiniProfiler.Prepare();
			}
			catch (Exception ex)
			{
				try
				{
					// We are not doing anything with the exception, because
					// we can't dare to try to save a log or soemthing, as
					// that could throw another exception... and we would be
					// in the same case. We just disable profiling.
					Settings.Enabled = false;
					Settings.Running = false;

					Logger.Log(ex);
					Logger.Log("Profiler couldn't start. Disabling everything.");
				}
				catch
				{
					// We CAN'T blow here, as we would shutdown the application
					// Profiling won't start, and the console will reflect the error
				}
			}
		}

		public static MiniProfiler Start()
        {
			if (!Settings.Running || !Settings.Enabled)
				return null;

            Settings.EnsureProfilerProvider();
            MiniProfiler startedProfiler = Settings.ProfilerProvider.Start();
					
			return startedProfiler;
        }

        public static void Stop()
        {
            Settings.EnsureProfilerProvider();
            Settings.ProfilerProvider.Stop(false);
        }

		public static IDisposable StepStatic(string name)
		{
			return Current.Step(name, "");
		}
		
		public static IDisposable StepStatic(string name, string sql)
        {
            return Current.Step(name, sql);
        }

        public override bool Equals(object other)
        {
            return other is MiniProfiler && Id.Equals(((MiniProfiler)other).Id);
        }

		public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

		public static void SuspendStorage(Exception ex)
		{
			MiniProfiler.StorageSuspended = true;

			if (Settings.StorageResumeMins.ToInt() > 0)
			{
				_resumeStorageTimer = new Timer(Int32.MaxValue);
				_resumeStorageTimer.AutoReset = false;
				_resumeStorageTimer.Interval = Settings.StorageResumeMins * ONE_MINUTE;
				_resumeStorageTimer.Elapsed += ResumeStorageTimer_Elapsed;
				_resumeStorageTimer.Enabled = true;

				Logger.Log("Storage suspended. Will try to resume in " + Settings.StorageResumeMins + " minutes.");
			}
			else
			{
				Logger.Log("Storage suspended. Will not try to resume later, since there's no value for ResumeStorageMinutes setting.");
			}
		}

		public static void ResumeStorage()
		{
			if (MiniProfiler.StorageSuspended)
			{
				MiniProfiler.StorageSuspended = false;
				Logger.Log("Storage resumed.");

				// Destroy auto-resume timer
				if (_resumeStorageTimer != null)
				{
					_resumeStorageTimer.Stop();
					_resumeStorageTimer.Enabled = false;
					_resumeStorageTimer = null;
				}
			}
		}

		public static void Prepare()
		{
			Logger.Log("Initializing...");

			if (Settings.Enabled)
			{
				// Set startup enabled state based on config
				Settings.Running = Settings.RunOnStartup;

				// Start scheduler
				Scheduler.Start();

				Logger.Log("Profiler ENABLED.");
			}
			else
			{
				// Stop profiler
				Settings.Running = false;

				// Stop scheduler
				Scheduler.Stop();

				Logger.Log("Profiler DISABLED.");
			}
		}

		internal IDisposable StepImpl(string name)
		{
			return StepImpl(name, "");
		}

        internal IDisposable StepImpl(string name, string sql)
        {
            Timing timing = new Timing(this, name, sql);

			this.Timings.Add(timing);

			return timing;
        }

        internal bool StopImpl()
        {
            if (!_sw.IsRunning)
                return false;

            _sw.Stop();

			DurationMilliseconds = GetRoundedMilliseconds(ElapsedTicks);

            foreach (var timing in this.Timings)
                timing.Stop();

            return true;
        }
		#endregion

		#region Misc
		internal decimal GetRoundedMilliseconds(long ticks)
        {
            long z = 10000 * ticks;
            decimal timesTen = (int)(z / _sw.Frequency);
            return timesTen / 10;
        }

        internal decimal GetDurationMilliseconds(long startTicks)
        {
            return GetRoundedMilliseconds(ElapsedTicks - startTicks);
		}
		#endregion
	}
}