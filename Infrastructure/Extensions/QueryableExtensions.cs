using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> IncludeRange<T>(this IQueryable<T> query, IEnumerable<string> includes)
        where T : class
    {
        if (includes == null)
        {
            return query;
        }

        foreach (string include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
