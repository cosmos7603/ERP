using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AM.DAL.DirectAccess
{
	public static class DatabaseExtensions
	{
		#region ExecuteDataTable
		public static DataTable ExecuteDataTable(this Database database, string storedProcedueName, params object[] p)
		{
			DataSet ds = database.ExecuteDataSet(storedProcedueName, p);

			if (ds.Tables.Count > 0)
				return ds.Tables[0];

			return null;
		}

		public static DataTable ExecuteDataTable(this Database database, DbTransaction dbTransaction, string storedProcedueName, params object[] p)
		{
			DataSet ds = database.ExecuteDataSet(dbTransaction, storedProcedueName, p);

			if (ds.Tables.Count > 0)
				return ds.Tables[0];

			return null;
		}
		#endregion

		#region ExecuteEnumerable
		public static IEnumerable<dynamic> ExecuteEnumerable(this Database database, string storedProcedureName, params object[] p)
		{
			using (DbCommand command = database.GetStoredProcCommand(storedProcedureName, p))
			{
				// No timeout
				command.CommandTimeout = 0;

				// Execute commant to get dataset result.
				DataSet ds = database.ExecuteDataSet(command);

				// Return dataset!
				if (ds.Tables.Count > 0)
					return ds.Tables[0].AsDynamicEnumerable();
			}

			return null;
		}

		public static IEnumerable<dynamic> ExecuteEnumerable(this Database database, string storedProcedureName, out int rowCount, params object[] p)
		{
			using (DbCommand command = database.GetStoredProcCommand(storedProcedureName, p))
			{
				// No timeout
				command.CommandTimeout = 0;
	
				// Execute commant to get dataset result.
				DataSet ds = database.ExecuteDataSet(command);

				// Get output parameter.
				rowCount = (int)command.Parameters["@RETURN_VALUE"].Value;

				// Return dataset!
				if (ds.Tables.Count > 0)
					return ds.Tables[0].AsDynamicEnumerable();
			}

			return null;
		}
		#endregion

		#region ExecuteDataRow
		public static DataRow ExecuteDataRow(this Database database, string storedProcedueName, params object[] p)
		{
			DataSet ds = database.ExecuteDataSet(storedProcedueName, p);

			if (ds.Tables.Count > 0)
				if (ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0].Rows[0];

			return null;
		}

		public static DataRow ExecuteDataRow(this Database database, DbCommand cmd)
		{
			DataSet ds = database.ExecuteDataSet(cmd);

			if (ds.Tables.Count > 0)
				if (ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0].Rows[0];

			return null;
		}

		public static DataRow ExecuteDataRow(this Database database, DbTransaction dbTransaction, string storedProcedueName, params object[] p)
		{
			DataSet ds = database.ExecuteDataSet(dbTransaction, storedProcedueName, p);

			if (ds.Tables.Count > 0)
				if (ds.Tables[0].Rows.Count > 0)
					return ds.Tables[0].Rows[0];

			return null;
		}
		#endregion
	}
}
