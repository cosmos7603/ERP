using System;
using System.Linq;
using System.Xml;
using System.IO;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
using Corpnet.SQLDeploy.NumericalSorting;
using System.Reflection;
using System.Globalization;

namespace Corpnet.SQLDeploy
{
	public class Program
	{
		#region Members
		public static string ConnectionString = "server=190.2.43.140,14433; database=CruiseWeb; Integrated Security=false; user=cwuser; password=AgMate1!;";
		public static string BasePath = "C:\\Projects\\TravelLeaders\\Site\\";
		public static List<RenameDefinition> TableRenameList;
		public static List<RenameDefinition> ColumnRenameList;
		#endregion

		#region Main
		static void Main(string[] args)
		{
			Console.WriteLine("========================================================");
			Console.WriteLine("Renamer v" + Assembly.GetExecutingAssembly().GetName().Version);
			Console.WriteLine("========================================================");
			Console.WriteLine();

			// Get keywords
			//ProcessKeywords();
			
			// First, process migration scripts
			//ProcessRenamings();

			// Reapply procedures
			ProcessProcedures();

			// Reapply procedures
			//ApplyAll();

			// Pause
			Console.WriteLine("\n\nProcess Finished...");
			Console.ReadLine();
		}
		#endregion

		#region Process

		#region Get Keywords
		private static void ProcessKeywords()
		{
			var kw = new List<string>();

			foreach (var file in Directory.GetFiles(BasePath + "\\DataBase\\Stored Procedures\\", "*.sql", SearchOption.AllDirectories))
				kw.AddRange(GetKeywords(file));

			foreach (var file in Directory.GetFiles(BasePath + "\\DataBase\\Functions\\", "*.sql", SearchOption.AllDirectories))
				kw.AddRange(GetKeywords(file));

			foreach (var file in Directory.GetFiles(BasePath + "\\Business\\", "*.cs", SearchOption.AllDirectories))
				kw.AddRange(GetKeywords(file));

			foreach (var file in Directory.GetFiles(BasePath + "\\Reports\\", "*.rdl", SearchOption.AllDirectories))
				kw.AddRange(GetKeywords(file));

			foreach (var file in Directory.GetFiles(BasePath + "\\WebSite\\", "*.cs", SearchOption.AllDirectories))
				kw.AddRange(GetKeywords(file, true));

			kw = ((List<string>)kw).Distinct().ToList();

			Console.WriteLine("Adding: " + kw.Count().ToString() + " keywords.");
			Console.WriteLine();
			Console.WriteLine();

			foreach (string s in kw)
			{
				Console.WriteLine("EXEC CruiseWeb_Rename.dbo.AddKeyword '" + s + "'");
			}
		}

		private static List<string> GetKeywords(string fn)
		{
			return GetKeywords(fn, false);
		}

		private static List<string> GetKeywords(string fn, bool inQuotes)
		{
			var kw = new List<string>();

			if (fn.IndexOf("\\CB_") != -1)
				return kw;

			string[] lines = File.ReadAllLines(fn);
			string[] wordDelimiters = new string[] { " ", ",", "-", ".", "\\", "/", "=", "+", "\"", "'", ")", "(", "[", "]", "\t", "!", "$", "&", "?", "*", "{", "}", ";", "<", ">"};

			foreach (string line in lines)
			{
				// For RDL, only take parameters
				if (fn.ToLower().IndexOf(".rdl") != -1)
				{
					var validLine = false;

					if (line.IndexOf("Field Name=") != -1) validLine = true;
					if (line.IndexOf("<QueryParameter") != -1) validLine = true;
					if (line.IndexOf("<DataField>") != -1) validLine = true;
					if (line.IndexOf("< Value >= Parameters!") != -1) validLine = true;

					if (!validLine)
						continue;
				}

				// Do not parse CB procedures
				if (fn.ToLower().IndexOf(".sql") != -1)
				{
					if (fn.IndexOf("\\CB_") != -1 || fn.IndexOf("\\u_CB_") != -1)
                        continue;

					if (fn.IndexOf("\\WinCruise_") != -1 || fn.IndexOf("\\u_WinCruise_") != -1)
						continue;

					if (fn.IndexOf("\\FileMaker_") != -1 || fn.IndexOf("\\u_FileMaker_") != -1)
						continue;
				}

				string[] words = line.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

				foreach (string w in words)
				{
					string tw = w.Trim();

					// Exclude 
					if (tw.StartsWith("u_")) continue;
					if (tw.StartsWith("ELMAH")) continue;
					if (tw.StartsWith("ANSI_WARNINGS")) continue;
					if (tw.StartsWith("SCOPE_IDENTITY")) continue;
					if (tw.StartsWith("XACT_ABORT")) continue;
					if (tw.StartsWith("IDENITTY_INSERT")) continue;
					if (tw.ToUpper().StartsWith("@@FETCH_STATUS")) continue;
					if (tw.StartsWith("FORWARD_ONLY")) continue;
					if (tw.StartsWith("READ_ONLY")) continue;
					if (tw.StartsWith("IDENITTY_INSERT")) continue;
					if (tw.StartsWith("QUOTED_IDENTIFIER")) continue;
					if (tw.StartsWith("sp_")) continue;
					if (tw.StartsWith("z_")) continue;
					if (tw.ToLower().StartsWith("object_id")) continue;
					if (tw.StartsWith("#")) continue;
					if (tw.StartsWith("CB_")) continue;
					if (tw.StartsWith("__")) continue;
					if (tw.StartsWith("YyyyMMddHHmmssfff_")) continue;
					if (tw.StartsWith("yyyyMMddHHmmssfff_")) continue;
                    if (tw.ToLower().StartsWith("_ddl")) continue;
					if (tw.ToLower().StartsWith("_lst")) continue;
					if (tw.ToLower().StartsWith("_dropbox")) continue;
					if (tw.ToLower().StartsWith("_dt")) continue;
					if (tw.ToLower().StartsWith("_due")) continue;
					if (tw.ToLower().StartsWith("_itemscount_")) continue;
					if (tw.ToLower().StartsWith("_hiddenpaxnum_")) continue;
					if (tw.ToLower().StartsWith("_paxnum_")) continue;
					if (tw.ToLower().StartsWith("_persister")) continue;
					if (tw.ToUpper().StartsWith("SQL_LATIN1_GENERAL_CP1_CI_AS")) continue;
					if (tw.ToLower().StartsWith("is_ms_shipped")) continue;
					if (tw.ToLower().StartsWith("parent_obj")) continue;
					if (tw == "_") continue;
					
					// Exclude all caps
					if (tw.ToUpper() == tw) continue;

					// No underscore?
					if (tw.IndexOf("_") == -1)
						continue;

					// In quotes?
					if (inQuotes && line.IndexOf("\"" + tw + "\"") == -1)
						continue;

					kw.Add(tw);
				}
			}

			return kw;
		}
		#endregion

		#region Process Procedures "U"
		private static void ProcessProcedures()
		{
			Console.WriteLine();

			// Removing u_
			Console.WriteLine("Removing U from C#");

			FindAndReplace(BasePath + "CwAPI", "*.cs", "\"u_", "\"");
			Console.Write(".");

			FindAndReplace(BasePath + "CwInterface", "*.cs", "\"u_", "\"");
			Console.Write(".");

			FindAndReplace(BasePath + "Business", "*.cs", "\"u_", "\"");
			Console.Write(".");

			FindAndReplace(BasePath + "Business", "*.cs", "[u_", "[");
			Console.Write(".");

			FindAndReplace(BasePath + "Business", "*.cs", "EXEC u_", "EXEC ");
			Console.Write(".");

			FindAndReplace(BasePath + "DataBase\\", "*.csproj", "\\u_", "\\");
			Console.Write(".");

			FindAndReplace(BasePath + "WebSite\\Config\\", "*.xml", "\"u_", "\"");
			Console.Write(".");

			Console.WriteLine();

			Console.WriteLine("Removing U from procedures");

			// get all the files we want and loop through them
			foreach (var file in Directory.GetFiles(BasePath + "\\DataBase\\Stored Procedures\\", "u_*.sql", SearchOption.AllDirectories))
			{
				// open, replace, overwrite
				var contents = File.ReadAllText(file);
				var newContent = contents;

				newContent = newContent.Replace("[u_", "[");
				newContent = newContent.Replace("EXEC u_", "EXEC ");
				newContent = newContent.Replace("Exec u_", "Exec ");
				newContent = newContent.Replace("EXECUTE u_", "EXECUTE ");
				newContent = newContent.Replace("exec u_", "exec ");
				newContent = newContent.Replace("execute u_", "execute ");
				newContent = newContent.Replace(".u_", ".");
				newContent = newContent.Replace("= u_", "= ");
				newContent = newContent.Replace("=u_", "=");
				newContent = newContent.Replace(": u_", ": ");
				newContent = newContent.Replace("procedure u_", "procedure ");
				newContent = newContent.Replace("PROCEDURE u_", "PROCEDURE ");
				newContent = newContent.Replace("Procedure u_", "Procedure ");
				newContent = newContent.Replace("procedure\tu_", "procedure\t");
				newContent = newContent.Replace("PROCEDURE\tu_", "PROCEDURE\t");
				newContent = newContent.Replace("Procedure\tu_", "Procedure\t");
				newContent = newContent.Replace(": u_", ": ");

				// only write if different
				if (contents != newContent)
					File.WriteAllText(file, newContent);

				try
				{
					var dest = file.Replace("u_", "");

					if (File.Exists(dest))
						File.Delete(dest);

					File.Move(file, dest);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error moving proc: " + file + " (" + ex.Message + ")");
                }
			}
		}
		#endregion

		#region Apply All Procedures
		private static void ApplyAll()
		{
			Console.WriteLine("Reapplying all functions");

			// get all the files we want and loop through them
			foreach (var file in Directory.GetFiles(BasePath + "\\DataBase\\Functions\\", "*.sql", SearchOption.AllDirectories))
			{
				Console.WriteLine("Applying: " + file);

				// open, replace, overwrite
				var contents = File.ReadAllText(file);

				try
				{
					ExecuteSQL(contents);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error applying proc: " + file + " (" + ex.Message + ")");
				}
			}

			// get all the files we want and loop through them
			Console.WriteLine("Reapplying all procedures");

			foreach (var file in Directory.GetFiles(BasePath + "\\DataBase\\Stored Procedures\\", "*.sql", SearchOption.AllDirectories))
			{
				Console.WriteLine("Applying: " + file);

				// open, replace, overwrite
				var contents = File.ReadAllText(file);
			
				try
				{
					ExecuteSQL(contents);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error applying proc: " + file + " (" + ex.Message + ")");
				}
			}
		}
		#endregion

		#region Renamings
		private static void ProcessRenamings()
		{
			ColumnRenameList = GetColumns();

			Console.WriteLine("Count: " + ColumnRenameList.Count().ToString());

			FindAndReplace(BasePath + "WebSite\\Config", "*.xml", ColumnRenameList, true);
			FindAndReplace(BasePath + "Business", "*.cs", ColumnRenameList, true);
			FindAndReplace(BasePath + "WebSite", "*.cs", ColumnRenameList, true);
			FindAndReplace(BasePath + "WebSite", "*.ascx", ColumnRenameList, true);
			FindAndReplace(BasePath + "WebSite", "*.aspx", ColumnRenameList, true);
			FindAndReplace(BasePath + "CwAPI", "*.cs", ColumnRenameList, true);
			FindAndReplace(BasePath + "CwInterface", "*.cs", ColumnRenameList, true);
			FindAndReplace(BasePath + "Database", "*.sql", ColumnRenameList, true);
			FindAndReplace(BasePath + "Reports", "*.rdl", ColumnRenameList, true);
			
			Console.WriteLine();
		}
		#endregion

		#endregion

		#region SQL
		private static IEnumerable ExecuteEnumerable(string sql)
		{
			SqlConnection sqlConnection = new SqlConnection(ConnectionString);
			DataSet ds = new DataSet();

			try
			{
				sqlConnection = new SqlConnection(ConnectionString);
				sqlConnection.Open();

				SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = sql;
				sqlCommand.CommandTimeout = 0;

				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = sqlCommand;
				da.Fill(ds);

				sqlConnection.Close();
				sqlConnection.Dispose();
			}
			catch (Exception ex)
			{
				sqlConnection.Close();
				sqlConnection.Dispose();

				throw ex;
			}

			return ds.Tables[0].AsEnumerable();
		}

		private static List<string> ExecuteList(string sql)
		{
			SqlConnection sqlConnection = new SqlConnection(ConnectionString);

			List<string> list = new List<string>();

			try
			{
				sqlConnection = new SqlConnection(ConnectionString);
				sqlConnection.Open();

				SqlCommand sqlCommand = sqlConnection.CreateCommand();

				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.CommandText = sql;
				sqlCommand.CommandTimeout = 0;

				SqlDataReader reader = sqlCommand.ExecuteReader();

				while (reader.Read())
					list.Add(reader[0].ToString());

				reader.Close();

				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				sqlConnection.Close();
				sqlConnection.Dispose();

				throw ex;
			}

			return list;
		}

		private static void ExecuteSQL(string sql)
		{
			SqlConnection sqlConnection = new SqlConnection(ConnectionString);

			sqlConnection.Open();

			SqlCommand sqlCommand = sqlConnection.CreateCommand();

			// Begin tran
			sqlCommand.CommandText = "SET XACT_ABORT ON; BEGIN TRAN;";
			sqlCommand.ExecuteNonQuery();
			try
			{
				// Parse all GO chunks, and execute them
				string[] chunks = Regex.Split(sql, Environment.NewLine + "GO", RegexOptions.IgnoreCase);

				foreach (string chunk in chunks)
				{
					// Exclude blank chunks
					if (chunk == "")
						continue;

					sqlCommand = sqlConnection.CreateCommand();

					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.CommandText = chunk;
					sqlCommand.CommandTimeout = 0;

					sqlCommand.ExecuteNonQuery();
				}

				// Begin tran
				sqlCommand.CommandText = "IF @@TRANCOUNT > 0 COMMIT TRAN";
				sqlCommand.CommandTimeout = 0;
				sqlCommand.ExecuteNonQuery();

				// Close connection
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				//Console.WriteLine("SQL=" + sqlCommand.CommandText);

				sqlConnection.Close();
				sqlConnection.Dispose();

				throw ex;
			}
		}
		#endregion

		#region Helpers
		static void FindAndReplace(string path, string pattern, string findThis, string replaceWithThis)
		{
			// get all the files we want and loop through them
			foreach (var file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories))
			{
				// open, replace, overwrite
				var contents = File.ReadAllText(file);
				var newContent = contents.Replace(findThis, replaceWithThis);

				// only write if different
				if (contents != newContent)
					File.WriteAllText(file, newContent);
			}
		}

		static void FindAndReplace(string path, string pattern, List<RenameDefinition> rdList, bool caseSensitive)
		{
			int i = 0;

			Console.WriteLine(path + "\\" + pattern);

			// get all the files we want and loop through them
			foreach (var file in Directory.GetFiles(path, pattern, SearchOption.AllDirectories))
			{
				i++;
				var contents = File.ReadAllText(file);
				var newContent = contents;

				foreach (RenameDefinition rd in rdList)
				{
					var words = GetMultipleCasingWords(rd.Old);

					foreach (string w in words)
						newContent = newContent.Replace(w, rd.New);

					//newContent = Regex.Replace(newContent, rd.Old, rd.New, RegexOptions.IgnoreCase);
				}

				// only write if different
				if (contents != newContent)
					File.WriteAllText(file, newContent);

				if (i % 100 == 0)
					Console.Write(".");
			}

			Console.WriteLine();
        }

		private static List<RenameDefinition> GetColumns()
		{
			string sql = File.ReadAllText(Config.TemplatesPath + "\\ListColumns.sql");
			var list = new List<RenameDefinition>();

			foreach (DataRow dr in ExecuteEnumerable(sql))
			{
				string columnName = dr["OldName"].ToString();
				string newName = dr["NewName"].ToString();

				list.Add(new RenameDefinition { Old = columnName, New = newName });
			}

			return list;
		}

		private static List<string> GetMultipleCasingWords(string w)
		{
			TextInfo ti = new CultureInfo("en-US", false).TextInfo;

            var parts = w.Split('_');
			var list = new List<string>();

			list.Add(w);

			if (parts.Length > 3)
				return list;

			if (parts.Length == 2)
			{
				list.Add(ti.ToTitleCase(parts[0]) + "_" + parts[1]);
				list.Add(parts[0] + "_" + ti.ToTitleCase(parts[1]));
				list.Add(ti.ToTitleCase(parts[0]) + "_" + ti.ToTitleCase(parts[1]));
			}
			else if(parts.Length == 3)
            {
				list.Add(ti.ToTitleCase(parts[0]) + "_" + parts[1] + "_" + parts[2]);
				list.Add(ti.ToTitleCase(parts[0]) + "_" + ti.ToTitleCase(parts[1]) + "_" + parts[2]);
				list.Add(ti.ToTitleCase(parts[0]) + "_" + parts[1] + "_" + ti.ToTitleCase(parts[2]));

				list.Add(ti.ToTitleCase(parts[0]) + "_" + ti.ToTitleCase(parts[1]) + "_" + parts[2]);
				list.Add(parts[0] + "_" + ti.ToTitleCase(parts[1]) + "_" + ti.ToTitleCase(parts[2]));
				list.Add(ti.ToTitleCase(parts[0]) + "_" + parts[1] + "_" + ti.ToTitleCase(parts[2]));

				list.Add(ti.ToTitleCase(parts[0]) + "_" + ti.ToTitleCase(parts[1]) + "_" + ti.ToTitleCase(parts[2]));
			}

			return list;
		}
		#endregion
	}
}
