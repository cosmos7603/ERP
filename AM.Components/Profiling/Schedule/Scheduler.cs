using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Corpnet.Profiling.Cron;
using Corpnet.Profiling.Config;
using Corpnet.Profiling.Extensions;
using Corpnet.Profiling.Interfaces;
using Corpnet.Profiling.Log;
using System.Diagnostics;

namespace Corpnet.Profiling.Schedule
{
	public class Scheduler
	{
		#region Consts
		private const int NO_FIRE = Int32.MaxValue;
		private const int ONE_MINUTE = 1000 * 60;
		#endregion

		#region Members
		private static Timer m_startTimer;
		private static Timer m_stopTimer;
		private static DateTime? m_runStarted;
		#endregion

		#region Properties
		public static DateTime? NextRunDate { get; set; }
		public static DateTime? NextStopDate { get; set; }
		#endregion

		#region Events
		private static void StartTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				// Log
				Logger.Log("Scheduler starting profiler...");

				// Start MP
				Settings.Running = true;

				// Save date & time when this run started
				m_runStarted = DateTime.Now;

				// Disable timer so that it doesn't runs again
				m_startTimer.Enabled = false;

				// Calculate next stop date
				NextStopDate = m_runStarted.Value.AddMinutes(Settings.RunDurationMins);

				// Run StopTimer
				m_stopTimer = new Timer(NO_FIRE);
				m_stopTimer.AutoReset = false;
				m_stopTimer.Interval = Settings.RunDurationMins * ONE_MINUTE;
				m_stopTimer.Elapsed += StopTimer_Elapsed;
				m_stopTimer.Enabled = true;
			}
			catch (Exception ex)
			{
				Logger.Log("Problem scheduling next run.", ex);
			}
		}

		private static void StopTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			// Log
			Logger.Log("Scheduler stopping profiler...");

			// Stop MP
			Settings.Running = false;

			// Prepare next round
			ScheduleNextRun();
		}
		#endregion

		#region Methods
		public static void Start()
		{
			// Log
			Logger.Log("Starting scheduler...");

			// No schedule?
			if (String.IsNullOrEmpty(Settings.RunScheduleCron) || Settings.RunDurationMins == 0)
				return;

			// Invalid schedule?
			if (Settings.InvalidSchedule)
				return;

			// Schedule next run
			ScheduleNextRun();
		}

		public static void Stop()
		{
			if (m_startTimer != null)
			{
				m_startTimer.Stop();
				m_startTimer.Enabled = false;
				m_startTimer = null;
			}

			if (m_stopTimer != null)
			{
				m_stopTimer.Stop();
				m_stopTimer.Enabled = false;
				m_stopTimer = null;
			}
		}

		private static void ScheduleNextRun()
		{
			// Destroy existing timers
			Stop();

			// Eliminate run started (no run now)
			m_runStarted = null;

			// Parse string for cron expression
			CronExpression cronExpression = null;

			try
			{
				cronExpression = new CronExpression(Settings.RunScheduleCron);
			}
			catch (Exception ex)
			{
				Logger.Log("Couldn't setup CRON expression.", ex);
			}

			// Calculate next run
			try
			{
				NextRunDate = cronExpression.GetNextValidTimeAfter(DateTime.Now).Value;
				NextStopDate = null;

				m_startTimer = new Timer(NO_FIRE);
				m_startTimer.AutoReset = false;
				m_startTimer.Interval = NextRunDate.Value.Subtract(DateTime.Now).TotalMilliseconds;
				m_startTimer.Elapsed += StartTimer_Elapsed;
				m_startTimer.Enabled = true;

				// Log
				Logger.Log("Next run scheduled succesfully for " + NextRunDate.ToString());

			}
			catch (Exception ex)
			{
				Logger.Log("Couldn't schedule next run.", ex);
			}
		}
		#endregion
	}
}
