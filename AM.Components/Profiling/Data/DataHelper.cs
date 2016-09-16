using System;
using System.Timers;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Corpnet.Profiling.Extensions;
using System.Configuration;
using System.Data;

namespace Corpnet.Profiling
{
	public class DataHelper
	{
		#region Methods
		public static DbConnection GetConnection()
		{
			return new SqlConnection(Settings.ConnectionString);
		}

		public static DbConnection GetOpenConnection()
		{
			var result = GetConnection();

			if (result.State != System.Data.ConnectionState.Open)
				result.Open();

			return result;
		}

		public static DataSet ExecuteDataSet(string commandText)
		{
			DbConnection connection = DataHelper.GetOpenConnection();
			DbCommand command = connection.CreateCommand();

			command.CommandText = commandText;
			command.CommandType = CommandType.Text;

			return ExecuteDataSet(command);
		}

		public static DataSet ExecuteDataSet(DbCommand command)
		{
			DataSet dataSet = new DataSet();
			
			using (DbDataAdapter adapter = new SqlDataAdapter())
            {
                ((IDbDataAdapter)adapter).SelectCommand = command;

				adapter.Fill(dataSet);
            }

			command.Connection.Dispose();
			command.Dispose();

			return dataSet;
        }

		public static DataRow ExecuteDataRow(string commandText)
		{
			DbConnection connection = DataHelper.GetOpenConnection();
			DbCommand command = connection.CreateCommand();

			command.CommandText = commandText;
			command.CommandType = CommandType.Text;

			return ExecuteDataSet(command).Tables[0].Rows[0];
		}

		public static void ExecuteNonQuery(string commandText)
		{
			DbConnection connection = DataHelper.GetOpenConnection();
			DbCommand command = connection.CreateCommand();

			command.CommandText = commandText;
			command.CommandType = CommandType.Text;

			command.ExecuteNonQuery();

			command.Connection.Dispose();
			command.Dispose();
		}
		#endregion
	}
}
