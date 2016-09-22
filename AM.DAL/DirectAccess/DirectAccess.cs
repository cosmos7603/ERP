using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using AM.Utils;

namespace AM.DAL.DirectAccess
{
	public class DirectAccess
	{
		#region Constructor
		static DirectAccess()
		{
			ConnectionName = "ERPEntities";
		}
        #endregion

        #region Properties
        public static string ConnectionName { get; set; }

		protected static Database Connection
		{
			get { return DatabaseFactory.CreateDatabase(ConnectionName); }
		}
		#endregion

		#region DB Helpers
		public static int ExecuteScalar(string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteScalar(storedProcedureName, p).ToInt();
		}

		public static void ExecuteNonQuery(string storedProcedureName, params object[] p)
		{
			Connection.ExecuteNonQuery(storedProcedureName, p);
		}

        public static DataSet ExecuteDataSet(string storedProcedureName, params object[] p)
		{
            DbCommand command = Connection.GetStoredProcCommand(storedProcedureName, p);
            command.CommandTimeout = 0;

            return Connection.ExecuteDataSet(command);
        }

		public static DataTable ExecuteDataTable(string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteDataTable(storedProcedureName, p);
		}

		public static DataTable ExecuteDataTable(string connectionStringName, string storedProcedureName, params object[] p)
		{
			Database connection = DatabaseFactory.CreateDatabase(connectionStringName);
			return connection.ExecuteDataTable(storedProcedureName, p);
		}

		public static DataRow ExecuteDataRow(string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteDataRow(storedProcedureName, p);
		}

		public static DataRow ExecuteDataRow(DbCommand cmd)
		{
			return Connection.ExecuteDataRow(cmd);
		}

		public static DataRow ExecuteDataRow(string connectionStringName, string storedProcedureName, params object[] p)
		{
			Database connection = DatabaseFactory.CreateDatabase(connectionStringName);
			return connection.ExecuteDataRow(storedProcedureName, p);
		}

		public static DataRow ExecuteDataRow(string connectionStringName, DbCommand cmd)
		{
			Database connection = DatabaseFactory.CreateDatabase(connectionStringName);
			return connection.ExecuteDataRow(cmd);
		}

		public static IEnumerable<dynamic> ExecuteEnumerable(string storedProcedureName, out int rowCount, params object[] p)
		{
			return Connection.ExecuteEnumerable(storedProcedureName, out rowCount, p);
		}

		public static IEnumerable<dynamic> ExecuteEnumerable(string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteEnumerable(storedProcedureName, p);
		}
		#endregion

		#region DB Helpers - Transactions
		public static int ExecuteScalar(DbTransaction dbTransaction, string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteScalar(dbTransaction, storedProcedureName, p).ToInt();
		}

		public static void ExecuteNonQuery(DbTransaction dbTransaction, string storedProcedureName, params object[] p)
		{
			Connection.ExecuteNonQuery(dbTransaction, storedProcedureName, p);
		}

		public static DataSet ExecuteDataSet(DbTransaction dbTransaction, string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteDataSet(dbTransaction, storedProcedureName, p);
		}

		public static DataTable ExecuteDataTable(DbTransaction dbTransaction, string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteDataTable(dbTransaction, storedProcedureName, p);
		}

		public static DataRow ExecuteDataRow(DbTransaction dbTransaction, string storedProcedureName, params object[] p)
		{
			return Connection.ExecuteDataRow(dbTransaction, storedProcedureName, p);
		}
		#endregion
    }
}
