using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace AM.DAL.DirectAccess
{
	public static class DataTableExtensions
	{
		#region Dynamic Enumerbles
		public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
		{
			return table.AsEnumerable().Select(row => new DynamicRow(row));
		}

		private sealed class DynamicRow : DynamicObject
		{
			private readonly DataRow m_row;

			internal DynamicRow(DataRow row) { m_row = row; }

			// Interprets a member-access as an indexer-access on the contained DataRow.
			public override bool TryGetMember(GetMemberBinder binder, out object result)
			{
				var retVal = m_row.Table.Columns.Contains(binder.Name);
				result = retVal ? m_row[binder.Name] : null;
				return retVal;
			}
		}
		#endregion
	}
}
