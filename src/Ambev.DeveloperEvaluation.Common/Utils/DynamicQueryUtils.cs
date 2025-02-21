using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Common.Utils;

public static class DynamicQueryUtils
{
    public static IQueryable<T> ApplyOrderByString<T>(IQueryable<T> query, string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return query;

        var orderByParts = orderBy.Split(',');

        foreach (var part in orderByParts)
        {
            var trimmedPart = part.Trim();
            var orderCriteria = trimmedPart.Split(' ');

            if (orderCriteria.Length != 2)
                throw new ArgumentException("Each sorting criterion must contain the property and the direction.");

            var property = orderCriteria[0];
            var direction = orderCriteria[1].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "desc" : "asc";

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameter, property);
            var lambda = Expression.Lambda(propertyExpression, parameter);

            var method = direction == "desc" ? "OrderByDescending" : "OrderBy";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { query.ElementType, propertyExpression.Type },
                query.Expression,
                Expression.Quote(lambda)
            );


            query = (IQueryable<T>)query.Provider.CreateQuery(resultExpression);
        }

        return query;
    }

    public static IQueryable<T> FilterStringWithAsterisk<T>(IQueryable<T> query,
        string filterValue,
        Expression<Func<T, string>> propertySelector)
    {
        if (string.IsNullOrEmpty(filterValue)) return query;

        if (filterValue.StartsWith("*") && filterValue.EndsWith("*"))
        {
            var searchTerm = filterValue.Trim('*');

            return query.Where(BuildPredicate(propertySelector, searchTerm, nameof(string.Contains)));
        }

        if (filterValue.StartsWith("*"))
        {
            var searchTerm = filterValue.TrimStart('*');
            return query.Where(BuildPredicate(propertySelector, searchTerm, nameof(string.StartsWith)));
        }

        if (filterValue.EndsWith("*"))
        {
            var searchTerm = filterValue.TrimEnd('*');
            return query.Where(BuildPredicate(propertySelector, searchTerm, nameof(string.EndsWith)));
        }

        return query.Where(BuildPredicate(propertySelector, filterValue, "Equals"));
    }

    public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        throw new ArgumentException("The provided expression is not a valid property selector.");
    }

    private static Expression<Func<T, bool>> BuildPredicate<T>(
       Expression<Func<T, string>> propertySelector,
       string filterValue,
       string methodName)
    {
        var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
        if (method == null) throw new InvalidOperationException($"Method '{methodName}' not found on type 'string'.");

        var parameter = propertySelector.Parameters[0];
        var propertyAccess = Expression.Call(propertySelector.Body, typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes));
        var filterConstant = Expression.Constant(filterValue.ToLower(), typeof(string));

        var methodCall = Expression.Call(propertyAccess, method, filterConstant);

        return Expression.Lambda<Func<T, bool>>(methodCall, parameter);
    }

}

