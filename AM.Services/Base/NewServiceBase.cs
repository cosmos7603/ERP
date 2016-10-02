using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using AM.DAL;
using AM.Services.Models;
using System.Runtime.Remoting.Messaging;
using AM.Services.Extensions;
using AM.Services.Grid;
using AM.Utils;
using AutoMapper;
using BLL.Helpers;
using DAL;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace AM.Services
{
	public abstract class NewServiceBase<T> : IService<T> where T : AuditableEntity
	{
		#region Consts
		private const string DBContextName = "DBContext";
		private const string IdentityContextName = "Identity";
		#endregion

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


		public virtual DataSourceResult GetAll(DataSourceRequest request)
		{
			try
			{
				//var sr = new ServiceResponse();
				DataSourceResult dataSourceResult;
				using (var unitOfWork = new UnitOfWork<T>())
				{
					try
					{
						dataSourceResult = unitOfWork.Repository.GetAll().ToDataSourceResult(request);
					}
					catch (Exception ex)
					{
						throw ex;
					}
					unitOfWork.Dispose();
				}
				//sr.Data = listEntities;
				//sr.ReturnValue = listEntities.Count;
				return dataSourceResult;
			}


			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual ServiceResponse Add(T poco)
		{
			try
			{
				var sr = Validate(poco);
				if (!sr.Status)
					return sr;

				using (var unitOfWork = new UnitOfWork<T>())
				{
					poco.CreateBy = "login";
					poco.EditBy = "login";
					poco.CreateDate = DateTime.Now;
					poco.EditDate = DateTime.Now;
					unitOfWork.Repository.Add(poco);
					unitOfWork.Save();
					sr.ReturnValue = poco.Id;
				}
				return sr;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual ServiceResponse Update(T poco, int id)
		{
			try
			{
				var sr = Validate(poco);

				if (!sr.Status)
					return sr;

				using (var unitOfWork = new UnitOfWork<T>())
				{
					poco.EditBy = "loginEdit";
					poco.EditDate = DateTime.Now;
					var entity = unitOfWork.Repository.GetById(id);
					Mapper.Map(poco, entity);
					unitOfWork.Save();
					sr.ReturnValue = entity.Id;
				}
				return sr;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public virtual ServiceResponse Delete(int id)
		{
			var sr = new ServiceResponse();
			try
			{

				using (var unitOfWork = new UnitOfWork<T>())
				{
					T entity = unitOfWork.Repository.GetById(id);
					unitOfWork.Repository.Delete(entity);
					unitOfWork.Save();
					sr.Data = id;
					sr.ReturnValue = id;
				}

			}
			catch (Exception ex)
			{
				sr.AddError(ex);
			}
			return sr;
		}

		public virtual List<T> GetPaged(IList<IFilterDescriptor> filters, IList<SortDescriptor> sort, int page, int rows, IList<AggregateDescriptor> aggregates, out int totalCount)
		{
			try
			{
				using (var unitOfWork = new UnitOfWork<T>())
				{
					totalCount = unitOfWork.Repository.GetAll().AsQueryable().Where(filters).Count();

					IQueryable<T> query = (IQueryable<T>)unitOfWork.Repository.GetAll()
						.AsQueryable()
						.Where(filters);
					if (sort != null && sort.Count > 0)
					{
						query = query.OrderBy(sort);
					}
					else
					{
						query = query.OrderByDescending(x => x.Id);
					}

					totalCount = query.Count();
					List<T> resultList = query.Skip((page - 1) * rows).Take(rows).ToList();

					//Mapper.CreateMap<T, T>();
					//return Mapper.Map<List<T>, List<T>>(resultList);

					return resultList;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public T GetById(int id)
		{
			try
			{
				//var poco = (T)Activator.CreateInstance(typeof(T));
				T entity;
				using (var unitOfWork = new UnitOfWork<T>())
				{
					entity = unitOfWork.Repository.GetById(id);
					//Mapper.CreateMap<T, T>();
					//poco = Mapper.Map<T, T>(entity);
					//Mapper.AssertConfigurationIsValid();
				}
				return entity;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public abstract ServiceResponse Validate(T model);

		#endregion
	}
}
