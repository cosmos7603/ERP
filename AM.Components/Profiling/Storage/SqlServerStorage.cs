using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using Corpnet.Profiling.Extensions;
using Corpnet.Profiling.Interfaces;
using System.Diagnostics;
using System.Threading;
using Corpnet.Profiling.Log;

namespace Corpnet.Profiling.Storage
{
	public class SqlServerStorage : IStorage
	{
		#region Structs
		[Serializable]
		public struct DbProfilingRecord
		{
			public int ProfilingId;
			public string Name;
			public string Url;
			public string Referer;
			public string Action;
			public string Event;
			public string ClientIP;
			public string ClientAgent;
			public string RequestID;
			public string ActiveUser;
			public string MachineName;
			public DateTime Started;
			public decimal Duration;
			public long ResponseSize;
			public long RequestSize;
			public string Exception;
			public string SessionID;
			public string ClientRequestID { get; set; }
			public decimal ClientTotalDuration { get; set; }
			public decimal ClientRedirectDuration { get; set; }
			public decimal ClientDnsDuration { get; set; }
			public decimal ClientConnectionDuration { get; set; }
			public decimal ClientRequestDuration { get; set; }
			public decimal ClientResponseDuration { get; set; }
			public decimal ClientDomDuration { get; set; }
			public decimal ClientLoadDuration { get; set; }
			public List<DbProfilingTimingRecord> Timings;
		}

		[Serializable]
		public struct DbProfilingTimingRecord
		{
			public int ProfilingId;
			public int TimingId;
			public string Name;
			public string SQL;
			public decimal StartOffset;
			public decimal? Duration;
			public long DataSize;
			public int DataRowCount;
		}
		#endregion

		#region Members
		private System.Timers.Timer m_saveTimer;
		private BlockingQueue<DbProfilingRecord> m_storageQueue = new BlockingQueue<DbProfilingRecord>();
		#endregion

		#region Properties
		public BlockingQueue<DbProfilingRecord> StorageQueue
		{
			get { return m_storageQueue; }
		}

		public long StorageQueueLength
		{
			get { return m_storageQueue.Count(); }
		}


		public long StorageQueueSize
		{
			get
			{
				if (Settings.Storage == null)
					return 0;

				if (Settings.StorageQueueInterval == 0)
					return 0;

				BlockingQueue<DbProfilingRecord> storageQueue = ((SqlServerStorage)(Settings.Storage)).StorageQueue;

				BinaryFormatter formatter = new BinaryFormatter();

				long storageSize = 0;

				using (MemoryStream memoryStream = new MemoryStream())
				{
					formatter.Serialize(memoryStream, storageQueue);
					storageSize = memoryStream.Length;
				}

				return storageSize;
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlServerStorage"/> class. 
		/// </summary>
		/// <param name="connectionString">
		/// The connection String.
		/// </param>
		public SqlServerStorage(string connectionString)
		{
			if (Settings.StorageQueueInterval > 0)
			{
				m_saveTimer = new System.Timers.Timer();
				m_saveTimer.Interval = Settings.StorageQueueInterval;
				m_saveTimer.Enabled = true;
				m_saveTimer.Elapsed += SaveTimer_Elapsed;
			}
		}

		void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (Monitor.TryEnter(m_saveTimer))
			{
				ProcessQueue();
				Monitor.Exit(m_saveTimer);
			}
		}

		private void ProcessQueue()
		{
			DbProfilingRecord dbProfilingRecord;

			while (m_storageQueue.Dequeue(out dbProfilingRecord))
			{
				SaveProfilingRecord(dbProfilingRecord);
			}
		}

		public void Save(MiniProfiler profiler)
		{
			if (MiniProfiler.StorageSuspended)
				return;

			DbProfilingRecord dbProfilingRecord = GetProfilingRecord(profiler);

			// If no queue, then save directly on the DB
			if (Settings.StorageQueueInterval == 0)
			{
				SaveProfilingRecord(dbProfilingRecord);
			}
			else
			{
				// Add to the queue for later processing
				m_storageQueue.Enqueue(dbProfilingRecord);
			}
		}

		private DbProfilingRecord GetProfilingRecord(MiniProfiler profiler)
		{
			// Profiling record
			DbProfilingRecord dbProfilingRecord = new DbProfilingRecord();

			dbProfilingRecord.Name = profiler.Name;
			dbProfilingRecord.Url = profiler.Url;
			dbProfilingRecord.Referer = profiler.Referer;
			dbProfilingRecord.Action = profiler.Action;
			dbProfilingRecord.Event = profiler.Event;
			dbProfilingRecord.ClientIP = profiler.ClientIP;
			dbProfilingRecord.ClientAgent = profiler.ClientAgent;
			dbProfilingRecord.RequestID = profiler.RequestID;
			dbProfilingRecord.ActiveUser = profiler.ActiveUser;
			dbProfilingRecord.MachineName = profiler.MachineName;
			dbProfilingRecord.Started = profiler.Started;
			dbProfilingRecord.Duration = profiler.DurationMilliseconds;
			dbProfilingRecord.ResponseSize = profiler.ResponseSize;
			dbProfilingRecord.RequestSize = profiler.RequestSize;
			dbProfilingRecord.Exception = profiler.Exception;
			dbProfilingRecord.SessionID = profiler.SessionID;

			// Client tracking
			dbProfilingRecord.ClientRequestID = profiler.ClientRequestID;
			dbProfilingRecord.ClientTotalDuration = profiler.ClientTotalDuration;
			dbProfilingRecord.ClientRedirectDuration = profiler.ClientRedirectDuration;
			dbProfilingRecord.ClientDnsDuration = profiler.ClientDnsDuration;
			dbProfilingRecord.ClientConnectionDuration = profiler.ClientConnectionDuration;
			dbProfilingRecord.ClientRequestDuration = profiler.ClientRequestDuration;
			dbProfilingRecord.ClientResponseDuration = profiler.ClientResponseDuration;
			dbProfilingRecord.ClientDomDuration = profiler.ClientDomDuration;
			dbProfilingRecord.ClientLoadDuration = profiler.ClientLoadDuration;

			dbProfilingRecord.Timings = new List<DbProfilingTimingRecord>();

			// Child timings
			foreach (var child in profiler.Timings)
			{
				DbProfilingTimingRecord dbProfilingTimingRecord = new DbProfilingTimingRecord();

				dbProfilingTimingRecord.Name = child.Name;
				dbProfilingTimingRecord.SQL = child.SQL;
				dbProfilingTimingRecord.StartOffset = child.StartMilliseconds;
				dbProfilingTimingRecord.Duration = child.DurationMilliseconds;
				dbProfilingTimingRecord.DataSize = child.DataSize;
				dbProfilingTimingRecord.DataRowCount = child.DataRowCount;

				dbProfilingRecord.Timings.Add(dbProfilingTimingRecord);
			}

			return dbProfilingRecord;
		}

		#region Database
		public void SaveProfilingRecord(DbProfilingRecord dbProfilingRecord)
		{
			const string sqlProfiling =
					@"INSERT INTO [Profiling]
					   (
					   [Name],
					   [Url],
					   [Referer],
					   [Action],
					   [Event],
					   [ClientIP],
					   [ClientAgent],
					   [RequestID],
					   [ActiveUser],
					   [MachineName],
					   [Started],
					   [Duration],
					   [ResponseSize],
   	  				   [RequestSize],
					   [Exception],
                       [SessionID])
					 VALUES (
					    @Name,
						@Url,
						@Referer,
						@Action,
						@Event,
						@ClientIP,
						@ClientAgent,
						@RequestID,
						@ActiveUser,
						@MachineName,
						@Started,
						@Duration,
						@ResponseSize,
						@RequestSize,
						@Exception,
                        @SessionID)
				
					SELECT SCOPE_IDENTITY()";

				const string sqlProfilingClient =
					@"INSERT INTO [ProfilingClient]
					   (
					   [RequestID],
					   [ClientTotalDuration],
					   [ClientRedirectDuration],
					   [ClientDnsDuration],
					   [ClientConnectionDuration],
					   [ClientRequestDuration],
					   [ClientResponseDuration],
					   [ClientDomDuration],
					   [ClientLoadDuration])
					 SELECT
					   @RequestID,
					   @ClientTotalDuration,
					   @ClientRedirectDuration,
					   @ClientDnsDuration,
					   @ClientConnectionDuration,
					   @ClientRequestDuration,
					   @ClientResponseDuration,
					   @ClientDomDuration,
					   @ClientLoadDuration
					 WHERE
						NOT EXISTS (SELECT 1 FROM ProfilingClient PC2 WHERE PC2.RequestID = @RequestID)";

			try
			{
				using (var conn = DataHelper.GetOpenConnection())
				{
					// Connection created succesfully? It should be the case always, but...
					if (conn == null)
						throw new Exception("Mini Profiler Error. Couldn't create connection to database. Suspending...");

					// Create command
					DbCommand cmd = conn.CreateCommand();

					// Command created succesfully?
					if (cmd == null)
						throw new Exception("Mini Profiler Error. Couldn't create command. Suspending...");

					cmd.CommandText = sqlProfiling;

					cmd.Parameters.Add(new SqlParameter("Name", dbProfilingRecord.Name.Truncate(64)));
					cmd.Parameters.Add(new SqlParameter("Url", dbProfilingRecord.Url.Truncate(256)));
					cmd.Parameters.Add(new SqlParameter("Referer", dbProfilingRecord.Referer.Truncate(256).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("Action", dbProfilingRecord.Action.Truncate(10).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("Event", dbProfilingRecord.Event.Truncate(128).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("ClientIP", dbProfilingRecord.ClientIP.Truncate(15).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("ClientAgent", dbProfilingRecord.ClientAgent.Truncate(128).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("RequestID", dbProfilingRecord.RequestID.Truncate(36).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("ActiveUser", dbProfilingRecord.ActiveUser.Truncate(30).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("MachineName", dbProfilingRecord.MachineName.Truncate(100)));
					cmd.Parameters.Add(new SqlParameter("Started", dbProfilingRecord.Started));
					cmd.Parameters.Add(new SqlParameter("Duration", dbProfilingRecord.Duration));
					cmd.Parameters.Add(new SqlParameter("ResponseSize", dbProfilingRecord.ResponseSize));
					cmd.Parameters.Add(new SqlParameter("RequestSize", dbProfilingRecord.RequestSize));
					cmd.Parameters.Add(new SqlParameter("Exception", dbProfilingRecord.Exception.Truncate(512).ToDBNull()));
					cmd.Parameters.Add(new SqlParameter("SessionID", dbProfilingRecord.SessionID.Truncate(24).ToDBNull()));

					// Capture Profiling ID
					dbProfilingRecord.ProfilingId = cmd.ExecuteScalar().ToInt();
					
					// Save client tracking
					if (dbProfilingRecord.ClientRequestID != null)
					{
						cmd = conn.CreateCommand();
						cmd.CommandText = sqlProfilingClient;
						cmd.Parameters.Add(new SqlParameter("RequestID", dbProfilingRecord.ClientRequestID));
						cmd.Parameters.Add(new SqlParameter("ClientTotalDuration", dbProfilingRecord.ClientTotalDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientRedirectDuration", dbProfilingRecord.ClientRedirectDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientDnsDuration", dbProfilingRecord.ClientDnsDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientConnectionDuration", dbProfilingRecord.ClientConnectionDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientRequestDuration", dbProfilingRecord.ClientRequestDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientResponseDuration", dbProfilingRecord.ClientResponseDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientDomDuration", dbProfilingRecord.ClientDomDuration.MaxDigits(6)));
						cmd.Parameters.Add(new SqlParameter("ClientLoadDuration", dbProfilingRecord.ClientLoadDuration.MaxDigits(6)));
						cmd.ExecuteNonQuery();
					}

					// Save timings
					if (dbProfilingRecord.Timings != null)
					{
						foreach (DbProfilingTimingRecord dbProfilingTimingRecord in dbProfilingRecord.Timings)
						{
							SaveTimingRecord(conn, dbProfilingRecord, dbProfilingTimingRecord);
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Log exception
				Logger.Log(ex);

				// Error? Suspend profiling
				MiniProfiler.SuspendStorage(ex);
			}
		}

		public void SaveTimingRecord(DbConnection connection, DbProfilingRecord profiler, DbProfilingTimingRecord timing)
		{
			const string sql =
				@"INSERT INTO [ProfilingTiming]
				   ([ProfilingId],
				   [Name],
				   [SQL],
				   [StartOffset],
				   [Duration],
				   [DataSize],
				   [DataRowCount])
				 VALUES (
					@ProfilingId,
					@Name,
					@SQL,
					@StartOffset,
					@Duration,
				    @DataSize,
				    @DataRowCount)

				SELECT SCOPE_IDENTITY()";

			using (var conn = DataHelper.GetOpenConnection())
			{
				// Connection created succesfully? It should be the case always, but...
				if (conn == null)
					throw new Exception("Mini Profiler Error. Couldn't create connection to database. Suspending...");

				// Create command
				DbCommand cmd = conn.CreateCommand();

				// Command created succesfully?
				if (cmd == null)
					throw new Exception("Mini Profiler Error. Couldn't create command. Suspending...");

				cmd.CommandText = sql;

				cmd.Parameters.Add(new SqlParameter("ProfilingId", profiler.ProfilingId));
				cmd.Parameters.Add(new SqlParameter("Name", timing.Name.Truncate(128)));
				cmd.Parameters.Add(new SqlParameter("SQL", timing.SQL.ToDBNull()));
				cmd.Parameters.Add(new SqlParameter("StartOffset", timing.StartOffset));
				cmd.Parameters.Add(new SqlParameter("Duration", timing.Duration));
				cmd.Parameters.Add(new SqlParameter("DataSize", timing.DataSize));
				cmd.Parameters.Add(new SqlParameter("DataRowCount", timing.DataRowCount));

				timing.TimingId = cmd.ExecuteScalar().ToInt();
			}
		}
		#endregion
	}
}
