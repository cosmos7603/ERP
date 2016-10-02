using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AM.DAL;
using AM.Services.Models;
using System.Runtime.Remoting.Messaging;
using AM.Services.Extensions;
using AM.Services.Grid;
using AM.Utils;
using AutoMapper;
using DAL;

namespace AM.Services
{
	public abstract class ServiceBase
	{
		#region Consts
		private const string DBContextName = "DBContext";
		private const string IdentityContextName = "Identity";
		#endregion

		protected static DB DB
		{
			get
			{
				// Find DB on this thread's bag
				DB db = CallContext.GetData(DBContextName) as DB;

				// If it doesn't exists, create it
				if (db == null)
				{
					db = new DB();

					// Save the context on the thread's bag
					CallContext.SetData(DBContextName, db);
				}

				return db;
			}
		}

		#region Properties
		protected static DBERP DBERP
		{
			get
			{
				// Find DB on this thread's bag
				DBERP db = CallContext.GetData(DBContextName) as DBERP;

				// If it doesn't exists, create it
				if (db == null)
				{
					db = new DBERP();

					// Save the context on the thread's bag
					CallContext.SetData(DBContextName, db);
				}

				return db;
			}
		}


		protected static ServiceUserIdentity Identity => CallContext.GetData(IdentityContextName) as ServiceUserIdentity ?? new ServiceUserIdentity
		{
			AuditLogin = "system"
		};

		#endregion

		#region Service Entities
		public static void SetIdentity(ServiceUserIdentity identity)
		{
			CallContext.SetData(IdentityContextName, identity);
		}
		#endregion

		#region Context Management
		public static void ResetContext()
		{
			// Remove and dispose current context
			DisposeContext();

			// Create new context
			CallContext.SetData(DBContextName, new DBERP());
		}

		public static void DisposeContext()
		{
			// Read current context
			DBERP db = CallContext.GetData(DBContextName) as DBERP;

			// If there's a context
			if (db != null)
			{
				// Dispose it.. freeing any connection and resources used
				db.Dispose();

				// Remove context instance from the thread's bag
				CallContext.SetData(DBContextName, null);
			}
		}

		protected static List<Filter> GetFilters(string searchVal)
		{

			double n;
			bool isNumeric = double.TryParse(searchVal, out n);

			PropertyInfo[] properties;
			if (isNumeric)
			{
				properties = typeof(DAL.Client).GetProperties()
			   .ToArray();
			}
			else
			{
				properties = typeof(DAL.Client).GetProperties().
				   Where(a => a.PropertyType == typeof(string))
			   .ToArray();
			}

			var filters = new List<Filter>();

			if (!String.IsNullOrEmpty(searchVal))
			{
				foreach (var prop in properties)
				{
					ComparisonType comparisonType;

					if (prop.PropertyType == typeof(string))
					{
						comparisonType = ComparisonType.Contains;
						filters.Add(
								new Filter
								{
									Comparison = comparisonType,
									Property = prop,
									Value = searchVal
								}
								);
					}
					else if (prop.PropertyType.IsNumeric())
					{
						comparisonType = ComparisonType.Equal;
						filters.Add(
					new Filter
					{
						Comparison = comparisonType,
						Property = prop,
						Value = searchVal.ToInt()
					}
				);
					}
				}

			}

			return filters;

		}


		//public virtual ServiceResponse GetAll()
		//{
		//	try
		//	{
		//		var sr = new ServiceResponse();
		//		List<T> listEntities;
		//		using (var unitOfWork = new UnitOfWork<T>())
		//		{
		//			listEntities = unitOfWork.Repository.GetAll().ToList();
		//		}
		//		sr.Data = listEntities;
		//		sr.ReturnValue = listEntities.Count;
		//		return sr;
		//	}


		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}

	

		#endregion
	}
}
