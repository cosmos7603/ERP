using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corpnet.Profiling.Log
{
	public class Logger
	{
		#region Log
		public static bool ValidateLog()
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return false;

			if (!Directory.Exists(Path.GetDirectoryName(Settings.LogFileName)))
				return false;

			return true;
		}

		public static void Log(string message)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return;

			string logLine = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss") + " - " + Environment.MachineName + " - ";

			logLine += message;

			File.AppendAllText(Settings.LogFileName, logLine + Environment.NewLine);
		}

		public static void Log(string message, params object[] args)
		{
			Log(String.Format(message, args));
		}

		public static void Log(string message, Exception ex)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return;

			Log("EXCEPTION THROWN. " + message + ". EXCEPTION DETAILS: " + RemoveCRLF(ex.ToString()));
		}

		public static void Log(Exception ex)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return;

			Log("EXCEPTION THROWN: " + RemoveCRLF(ex.ToString()));
		}

		public static void LogError(string message)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return;

			Log("ERROR: " + message);
		}

		public static void ClearLog()
		{
			if (!String.IsNullOrEmpty(Settings.LogFileName))
				File.Delete(Settings.LogFileName);
		}
		#endregion

		#region Helpers
		private static string RemoveCRLF(string s)
		{
			string r = s;
			
			r = r.Replace("\r", " ");
			r = r.Replace("\n", " ");

			return r;
		}

		public static string[] Tail(int lineCount)
		{
			if (String.IsNullOrEmpty(Settings.LogFileName))
				return null;

			if (!File.Exists(Settings.LogFileName))
				return new string[] { "<span style='color: red'><b>Log file not found.</b></span>" };

			using (TextReader reader = File.OpenText(Settings.LogFileName))
			{
				var buffer = new List<string>(lineCount);
				string line;

				for (int i = 0; i < lineCount; i++)
				{
					line = reader.ReadLine();
					if (line == null) return buffer.ToArray();
					buffer.Add(line);
				}

				int lastLine = lineCount - 1;           //The index of the last line read from the buffer.  Everything > this index was read earlier than everything <= this indes

				while (null != (line = reader.ReadLine()))
				{
					lastLine++;
					if (lastLine == lineCount) lastLine = 0;
					buffer[lastLine] = line;
				}

				if (lastLine == lineCount - 1) return buffer.ToArray();
				var retVal = new string[lineCount];
				buffer.CopyTo(lastLine + 1, retVal, 0, lineCount - lastLine - 1);
				buffer.CopyTo(0, retVal, lineCount - lastLine - 1, lastLine + 1);

				return retVal;
			}
		}
		#endregion
	}
}
