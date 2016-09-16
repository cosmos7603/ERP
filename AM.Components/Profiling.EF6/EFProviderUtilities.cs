using System;
using System.Data;
using System.Data.Common;

namespace Corpnet.Profiling.EF6
{
    internal class EFProviderUtilities
    {
		public static string GetFormattedSqlCommand(string commandText, DbParameterCollection ps)
		{
			foreach (DbParameter p in ps)
			{
				string value = "";

				if (p.Value == null || p.Value == DBNull.Value)
				{
					value = "NULL";
				}
				else if (p.DbType == DbType.DateTime || p.DbType == DbType.DateTime2)
				{
					value = "'" + Convert.ToDateTime(p.Value).ToString("yyyy-MM-dd hh:mm:ss") + "'";
				}
				else if (p.DbType == DbType.Int16 || p.DbType == DbType.Int32 || p.DbType == DbType.Int64 || p.DbType == DbType.Decimal || p.DbType == DbType.Double || p.DbType == DbType.Single)
				{
					value = p.Value.ToString();
				}
				else if (p.DbType == DbType.String || p.DbType == DbType.StringFixedLength)
				{
					value = "'" + p.Value + "'";
				}
				else if (p.DbType == DbType.Boolean)
				{
					value = (Convert.ToBoolean(p.Value)) ? "1" : "0";
				}
				else
				{
					value = p.Value.ToString();
				}

				string paramName = p.ParameterName;

				if (paramName.IndexOf("@") != -1)
					commandText = commandText.Replace(p.ParameterName, value);
				else
					commandText = commandText.Replace("@" + p.ParameterName, value);
			}

			return commandText;
		}
    }
}
