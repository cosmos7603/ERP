using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BLL.Grid;

namespace BLL.Helpers
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, SortOptions sortOptions)
        {
            var param = Expression.Parameter(typeof(TEntity), "entity");
            PropertyInfo finalOrderField = null;

            Expression parent = param;

            foreach (var part in sortOptions.PropertyChain.Select(p => string.Format("{0}{1}", p.Substring(0, 1).ToUpperInvariant(), p.Substring(1))))
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

            if (sortOptions.Direction == OrderDirection.Ascending)
                return query.OrderBy(lambdaSort);

            return query.OrderByDescending(lambdaSort);
        }


        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> query, IList<Filter> filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
            Expression predicateBody = null;

            foreach (var filter in filters)
            {
                var propertyEntity = typeof(TEntity).GetProperty(filter.Property.Name);

                Expression left = Expression.Property(pe, propertyEntity);

                if (propertyEntity.PropertyType.Name.StartsWith("Nullable"))
                {
                    var nullType = propertyEntity.PropertyType;
                    Expression hasValueExp = Expression.Property(left, nullType.GetProperty("HasValue"));

                    predicateBody = predicateBody == null ? hasValueExp : Expression.And(predicateBody, hasValueExp);

                    left = Expression.Property(left, nullType.GetProperty("Value"));
                }

                Expression exp = null;

                Expression right;
                switch (filter.Value.GetType().Name)
                {
                    case "string":
                    case "String":
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var argExpType = Expression.Constant(filter.Value, typeof(string));

                        exp = Expression.Call(left, containsMethod, new Expression[] { argExpType });
                        break;
                    case "DateTime":
                        right = Expression.Constant(filter.Value, typeof(DateTime));

                        if (filter.Comparison == ComparisonType.Less)
                            exp = Expression.LessThanOrEqual(left, right);
                        else if (filter.Comparison == ComparisonType.Greater)
                            exp = Expression.GreaterThanOrEqual(left, right);
                        else exp = Expression.Equal(left, right);

                        break;
                    case "Int16":
                    case "Int32":
                    case "Int64":
                        right = Expression.Constant(filter.Value, typeof(int));

                        if (filter.Comparison == ComparisonType.Less)
                            exp = Expression.LessThanOrEqual(left, right);
                        else if (filter.Comparison == ComparisonType.Greater)
                            exp = Expression.GreaterThanOrEqual(left, right);
                        else exp = Expression.Equal(left, right);

                        break;
                }

                if (exp != null)
                    predicateBody = predicateBody == null ? exp : Expression.And(predicateBody, exp);
            }


            if (predicateBody == null) return null;

            var lambdaFilter = Expression.Lambda<Func<TEntity, bool>>(predicateBody, new[] { pe });

            return query.Where(lambdaFilter);
        }
    }
}
