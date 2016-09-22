using AM.DAL;
using AM.DAL.QueryResults;
using AM.Utils;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace AM.Services.Shared.Support
{
	public class ConsoleService : ServiceBase
	{
		#region Consts
		private const int SYSTEM_USER_ID = 1;
		private const int FILE_HOURS = -24;
		#endregion

		#region Lists
		public static ServiceResponse GetEventLogList(GetErrorsParameters p)
		{
			var sr = new ServiceResponse();

			var sqlParameters = new[]
			{
				new SqlParameter("StartDate", p.StartDate.ToDBNull()),
				new SqlParameter("PageSize", p.PageSize),
				new SqlParameter("PageIndex", p.PageIndex),
				new SqlParameter("SortField", p.SortField),
				new SqlParameter("SortOrder", p.SortDirection),
				new SqlParameter { ParameterName = "RowCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
			};

			var result = DB.ExecuteSp<EventLogGetListResult>("EventLog_GetList", sqlParameters);

			sr.ReturnValue = sqlParameters.Single(x => x.ParameterName == "RowCount").Value.ToInt();
			sr.Data = result;

			return sr;
		}

		public static ServiceResponse GetErrorList(GetErrorsParameters p)
		{
			var sr = new ServiceResponse();

			var sqlParameters = new[]
			{
				new SqlParameter("StartDate", p.StartDate.ToDBNull()),
				new SqlParameter("PageSize", p.PageSize),
				new SqlParameter("PageIndex", p.PageIndex),
				new SqlParameter("SortField", p.SortField),
				new SqlParameter("SortOrder", p.SortDirection),
				new SqlParameter { ParameterName = "RowCount", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
			};

			var result = DB.ExecuteSp<EventLogGetErrorsResult>("EventLog_GetErrors", sqlParameters);

			sr.ReturnValue = sqlParameters.Single(x => x.ParameterName == "RowCount").Value.ToInt();
			sr.Data = result;

			return sr;
		}

		public static ServiceResponse GetEmailLogList(GetEmailLogListParameters p)
		{
			var sr = new ServiceResponse();

			var query = DB
				.Email
				.Include(x => x.EmailAttachs)
				.Include(x => x.EmailAttachs.Select(d => d.DataFile))
				.AsNoTracking()
				.AsQueryable();

			query = query
				.OrderBy(p.SortField + " " + p.SortDirection)
				.Skip((p.PageIndex - 1) * p.PageSize)
				.Take(p.PageSize);

			sr.ReturnValue = query.Count();
			sr.Data = query.ToList();

			return sr;
		}
		#endregion

		#region Classes
		public class GetErrorsParameters : PagerParameters
		{
			public DateTime? StartDate { get; set; }
		}

		public class GetEmailLogListParameters : PagerParameters
		{
		}
		#endregion
	}
}