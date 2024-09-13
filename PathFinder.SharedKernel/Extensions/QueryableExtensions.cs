using System.Linq.Expressions;

namespace PathFinder.SharedKernel.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string orderProperty, bool isAsc = true, bool isOrderBy = true)
        {
            try
            {
                Expression expression = source.Expression;
                // { x }
                ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

                // You can include sorting directions for advanced cases
                string method;
                if (isOrderBy)
                    method = isAsc ? "OrderBy" : "OrderByDescending";
                else
                    method = isAsc ? "ThenBy" : "ThenByDescending";

                var propertyNames = orderProperty.Split(".");
                if (propertyNames.Length > 1)
                {
                    Expression property = parameter;
                    foreach (var prop in propertyNames)
                    {
                        property = Expression.PropertyOrField(property, prop);
                    }

                    LambdaExpression lambda = Expression.Lambda(property, parameter);
                    MethodCallExpression call = Expression.Call(
                    typeof(Queryable),
                    method,
                    new Type[] { typeof(T), property.Type },
                    expression,
                    Expression.Quote(lambda));

                    var returnquery = (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);

                    return returnquery;
                }
                else
                {
                    var selector = Expression.PropertyOrField(parameter, orderProperty);
                    LambdaExpression lambda = Expression.Lambda(selector, parameter);
                    expression = Expression.Call(
                        typeof(Queryable),
                        method,
                        new Type[] { source.ElementType, selector.Type },
                        expression,
                        Expression.Quote(lambda)
                    );

                    return source.Provider.CreateQuery<T>(expression);
                }
            }
            catch (Exception ex)
            {
                return source;
            }

        }

    }
}