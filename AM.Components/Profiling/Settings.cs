using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using Corpnet.Profiling.Config;
using Corpnet.Profiling.Extensions;
using Corpnet.Profiling.Interfaces;
using Corpnet.Profiling.Log;
using Corpnet.Profiling.Schedule;

namespace Corpnet.Profiling
{
	public static class Settings
	{
		#region Members
		private static ProfilingConfigurationSection _profilingConfigurationSection;
		private static DataTable m_profilingFilter;
		private static bool m_running;
		#endregion

		#region Constructors
		static Settings()
		{
		}
		#endregion

		#region Properties
		public static bool Enabled { get; set; }
		public static bool RunOnStartup { get; set; }
		public static string ActiveUserLocator { get; set; }
		public static string RunScheduleCron { get; set; }
		public static int RunDurationMins { get; set; }
		public static int StorageResumeMins { get; set; }
		public static int StorageQueueInterval { get; set; }
		public static bool InvalidSchedule { get; set; }
		public static IStorage Storage { get; set; }
		public static string Version { get; private set; }
		public static IProfilerProvider ProfilerProvider { get; set; }
		internal static Func<IStopwatch> StopwatchProvider { get; set; }

		public static string LogFileName
		{
			get
			{
				return ProfilingConfigurationSection.LogFileName;
			}
		}

		public static bool Running
		{
			get
			{
				return m_running;
			}
			set
			{
				if (!m_running && value == true)
				{
					Logger.Log("Profiler RUNNING.");
				}
				else if (m_running && value == false)
				{
					Logger.Log("Profiler STOPPED.");
				}
					
				m_running = value;
			}
		}

		public static ProfilingConfigurationSection ProfilingConfigurationSection
		{
			get
			{
				if (_profilingConfigurationSection == null)
					_profilingConfigurationSection = (ProfilingConfigurationSection)ConfigurationManager.GetSection("profilingConfiguration");

				return _profilingConfigurationSection;
			}
		}
		
		public static DataTable ProfilingFilter
		{
			get
			{
				if (m_profilingFilter == null)
					LoadProfilingFilters();

				return m_profilingFilter;
			}
			set
			{
				m_profilingFilter = value;
			}

		}

		public static string ConnectionString
		{
			get
			{
				if (ConfigurationManager.ConnectionStrings[ProfilingConfigurationSection.ConnectionStringName] == null)
					throw new Exception("ConnectionStringName for profiling doesn't exists.");

				return ConfigurationManager.ConnectionStrings[ProfilingConfigurationSection.ConnectionStringName].ToString();
			}
		}
		#endregion

		#region Methods
		public static void LoadProfilingFilters()
		{
			Logger.Log("Loading profiling filters...");

			try
			{
				m_profilingFilter = DataHelper.ExecuteDataSet("SELECT UrlString FROM ProfilingFilter").Tables[0];
			}
			catch (Exception ex)
			{
				// If there was a problem reading from the DB, return empty table
				m_profilingFilter = new DataTable();
				m_profilingFilter.Columns.Add("UrlString", typeof(string));

				// Log
				Logger.Log("Can't load Profiling Filters.", ex);
			}
		}

		public static void LoadProfilingConfig()
		{
			Logger.Log("Loading config...");

			try
			{
				DataRow drConfig = DataHelper.ExecuteDataRow("SELECT * FROM ProfilingConfig");

				Enabled = drConfig["Enabled"].ToBool();
				RunOnStartup = drConfig["RunOnStartup"].ToBool();
				RunScheduleCron = drConfig["RunScheduleCron"].ToString();
				RunDurationMins = drConfig["RunDurationMins"].ToInt();
				StorageQueueInterval = drConfig["StorageQueueInterval"].ToInt();
				StorageResumeMins = drConfig["StorageResumeMins"].ToInt();
				ActiveUserLocator = drConfig["ActiveUserLocator"].ToString();
				Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

				ValidateSchedule();

				LoadProfilingFilters();
			}
			catch (Exception ex)
			{
				Logger.Log(ex);
				Logger.Log("Can't read profiling configuration. Disabling profiler...");

				Enabled = false;
				RunOnStartup = false;
				RunScheduleCron = "";
			}

			Logger.Log("Config loaded. Version: " + Settings.Version);
			Logger.Log("ST: {0}, ROS: {1}, CRON: {2}, DUR: {3}, QUEUE: {4}, RES: {5}, USER: {6}, LOG: {7}", Enabled ? "Enabled" : "Disabled", RunOnStartup, RunScheduleCron, RunDurationMins, StorageQueueInterval, StorageResumeMins, ActiveUserLocator, LogFileName);
		}

		public static void EnsureProfilerProvider()
		{
			if (ProfilerProvider == null)
				ProfilerProvider = new WebRequestProfilerProvider();
		}

		public static void ValidateSchedule()
		{
			Logger.Log("Validating schedule...");

			InvalidSchedule = false;

			// There's a cron but there's no duration
			if (RunScheduleCron != "" && RunDurationMins == 0)
			{
				Logger.Log("Schedule setup error: there's a CRON expression, but there's no duration specified.");
				InvalidSchedule = true;
			}

			// There's a cron byt it's not valid
			if (RunScheduleCron != "" && !Cron.CronExpression.IsValidExpression(RunScheduleCron))
			{
				Logger.Log("Schedule setup error: there's a CRON expression, but it's not valid.");
				InvalidSchedule = true;
			}

			// Can't have RunOnStartup at the same time as the schedule
			if (RunOnStartup && !String.IsNullOrEmpty(RunScheduleCron))
			{
				Logger.Log("RunOnStartup can't be ON if there's an schedule setup. Use one or the other.");
				InvalidSchedule = true;
			}
		}

		public static void UpdateEnableStatus(bool status)
		{
			Logger.Log("Updating enabled status...");

			const string sql = @"UPDATE ProfilingConfig SET Enabled = {0}";

			// Update setting in database
			DataHelper.ExecuteNonQuery(String.Format(sql, (status ? "1" : "0")));

			// Reload config
			LoadProfilingConfig();

			// Re-initialize
			MiniProfiler.Prepare();
		}
		#endregion
	}
}
