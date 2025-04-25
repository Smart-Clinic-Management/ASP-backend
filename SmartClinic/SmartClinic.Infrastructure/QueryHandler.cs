using System.Linq.Expressions;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastructure;
public static class QueryHandler
{
    public static IQueryable<T> GetQuery<T>(this IQueryable<T> query, Expression<Func<T, bool>>? criteria = null,
        int? pageSize = null, int? pageIndex = null, bool withTracking = true, params string[] includes) where T : BaseEntity
    {

        criteria ??= x => true;
        query = query.Where(criteria);

        #region includes

        if (includes.Length != 0)
            foreach (var include in includes)
                query = query.Include(include);

        #endregion

        if (!withTracking)
            query = query.AsNoTracking();

        if (pageSize is not null && pageIndex is not null)
        {
            pageIndex = Math.Max(1, pageIndex!.Value);
            pageSize = pageSize > 0 && pageSize <= 20 ? pageSize : 5;
            int skip = (pageIndex.Value - 1) * pageSize.Value;

            query = query.Skip(skip).Take(pageSize.Value);
        }

        return query;
    }
}
