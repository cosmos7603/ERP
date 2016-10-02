using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Helpers;
using AM.Services.Grid;
using Kendo.Mvc;
using System.ComponentModel;
using AM.Utils;

namespace AM.Services.Extensions
{
	public static class LinqExtensions
	{
		public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, IList<SortDescriptor> sortOptions)
		{
			var param = Expression.Parameter(typeof(TEntity), "entity");
			PropertyInfo finalOrderField = null;

			Expression parent = param;

			foreach (var part in sortOptions.Select(p => string.Format("{0}{1}", p.Member.Substring(0, 1).ToUpperInvariant(), p.Member.Substring(1))))
			{
				parent = Expression.Property(parent, part);
				finalOrderField = finalOrderField == null
					? typeof(TEntity).GetProperty(part)
					: finalOrderField.GetType().GetProperty(part);
			}
			//           if (finalOrderField == null) return null;

			//if (finalOrderField.PropertyType.Name.StartsWith("Nullable"))
			//{
			//    var nullType = finalOrderField.PropertyType;
			//    parent = Expression.Call(parent, nullType.GetMethod("GetValueOrDefault", new Type[] { }));
			//}

			Expression conversion = Expression.Convert(parent, typeof(object));
			var lambdaSort = Expression.Lambda<Func<TEntity, object>>(conversion, param);

			if (sortOptions.Any())
			{
				foreach (SortDescriptor sortDescriptor in sortOptions)
				{
					if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
					{
						query = query.OrderBy(lambdaSort);
					}
					else
					{
						query = query.OrderByDescending(lambdaSort);
					}

					//query = query.OrderBy(sortDescriptor.Member);
				}
			}

			//if (sortOptions. == SortDirection.Ascending)
			

			return query;
		}


		//public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, IList<Filter> filters)
		//{
		//	if (filters == null || filters.Count == 0)
		//		return query;

		//	ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
		//	Expression predicateBody = null;

		//	foreach (var filter in filters)
		//	{
		//		var propertyEntity = typeof(TEntity).GetProperty(filter.Property.Name);

		//		Expression left = Expression.Property(pe, propertyEntity);

		//		if (propertyEntity.PropertyType.Name.StartsWith("Nullable"))
		//		{
		//			var nullType = propertyEntity.PropertyType;
		//			Expression hasValueExp = Expression.Property(left, nullType.GetProperty("HasValue"));

		//			predicateBody = predicateBody == null ? hasValueExp : Expression.And(predicateBody, hasValueExp);

		//			left = Expression.Property(left, nullType.GetProperty("Value"));
		//		}
		//		Expression exp = GetExpression(left, filter);
		//		if (exp != null)
		//			predicateBody = predicateBody == null ? exp : Expression.Or(predicateBody, exp);
		//	}


		//	if (predicateBody == null) return null;

		//	var lambdaFilter = Expression.Lambda<Func<TEntity, bool>>(predicateBody, new[] { pe });

		//	return query.Where(lambdaFilter);
		//}

		public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, IList<FilterDescriptor> filters)
		{
			if (filters == null || filters.Count == 0)
				return query;

			ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
			Expression predicateBody = null;

			foreach (var filter in filters)
			{
				var propertyEntity = typeof(TEntity).GetProperty(filter.Member);

				Expression left = Expression.Property(pe, propertyEntity);

				if (propertyEntity.PropertyType.Name.StartsWith("Nullable"))
				{
					var nullType = propertyEntity.PropertyType;
					Expression hasValueExp = Expression.Property(left, nullType.GetProperty("HasValue"));

					predicateBody = predicateBody == null ? hasValueExp : Expression.And(predicateBody, hasValueExp);

					left = Expression.Property(left, nullType.GetProperty("Value"));
				}
				Expression exp = GetExpression(left, filter);
				if (exp != null)
					predicateBody = predicateBody == null ? exp : Expression.Or(predicateBody, exp);
			}


			if (predicateBody == null) return null;

			var lambdaFilter = Expression.Lambda<Func<TEntity, bool>>(predicateBody, new[] { pe });

			return query.Where(lambdaFilter);
		}

		private static Expression GetExpression(Expression left, FilterDescriptor filter)
		{
			Expression exp = null;
			Expression right;
			switch (filter.Operator)
			{
				case FilterOperator.Contains:
					var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
					var argExpType = Expression.Constant(filter.Value, typeof(string));

					exp = Expression.Call(left, containsMethod, new Expression[] { argExpType });
					break;
				//case "DateTime":
				//	right = Expression.Constant(filter.Value, typeof(DateTime));

				//	if (filter.Comparison == ComparisonType.Less)
				//		exp = Expression.LessThanOrEqual(left, right);
				//	else if (filter.Comparison == ComparisonType.Greater)
				//		exp = Expression.GreaterThanOrEqual(left, right);
				//	else exp = Expression.Equal(left, right);

				//	break;
				//case "Int16":
				//case "Int32":
				//case "Int64":

				case FilterOperator.IsGreaterThan:
				case FilterOperator.IsGreaterThanOrEqualTo:
					right = Expression.Constant(filter.Value, typeof(int));
					exp = Expression.GreaterThanOrEqual(left, right);
					break;

				case FilterOperator.IsLessThan:
				case FilterOperator.IsLessThanOrEqualTo:

					right = Expression.Constant(filter.Value, typeof(int));
					exp = Expression.LessThanOrEqual(left, right);
					break;



				case FilterOperator.IsEqualTo:
					right = Expression.Constant(filter.Value, typeof(int));
					exp = Expression.Equal(left, right);
					break;
			}
			return exp;
		}


	}



}
