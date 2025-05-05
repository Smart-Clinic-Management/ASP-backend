using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using SmartClinic.Application.Bases;

namespace SmartClinic.Infrastructure;
public static class QueryHandler
{
    public static IQueryable<T> GetQuery<T>(this IQueryable<T> query, Expression<Func<T, bool>>? criteria = null,
        int? pageSize = null, int? pageIndex = null, bool withTracking = true, string? orderBy = null,
        bool descending = false, bool isDistinct = false, params string[] includes)
        where T : BaseEntity
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


        #region Order


        var orderDirection = descending ? " descending" : string.Empty;

        if (ValidProperty<T>(orderBy))
            query = query.OrderBy(orderBy + orderDirection);
        else
            query = descending ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id);

        #endregion


        if (isDistinct)
            query = query.Distinct();

        #region Paging
        if (pageSize is not null && pageIndex is not null)
        {
            pageIndex = Math.Max(1, pageIndex!.Value);
            pageSize = pageSize > 0 && pageSize <= 20 ? pageSize : 5;
            int skip = (pageIndex.Value - 1) * pageSize.Value;

            query = query.Skip(skip).Take(pageSize.Value);
        }
        #endregion

        return query;
    }



    public static IQueryable<TResult> GetQuery<T, TResult>(this IQueryable<T> query, Expression<Func<T, TResult>> select, Expression<Func<T, bool>>? criteria = null,
         int? pageSize = null, int? pageIndex = null, string? orderBy = null,
        bool descending = false, bool isDistinct = false)
        where T : BaseEntity
        where TResult : IDto
    {
        criteria ??= x => true;
        query = query.Where(criteria);


        var result = query.Select(select);


        #region Order


        var orderDirection = descending ? " descending" : string.Empty;

        if (ValidDtoProperty<TResult>(orderBy))
            result = result.OrderBy(orderBy + orderDirection);
        else
            result = descending ? result.OrderByDescending(x => x.Id) : result.OrderBy(x => x.Id);

        #endregion

        if (isDistinct)
            result = result.Distinct();

        #region Paging
        if (pageSize is not null && pageIndex is not null)
        {
            pageIndex = Math.Max(1, pageIndex!.Value);
            pageSize = pageSize > 0 && pageSize <= 20 ? pageSize : 5;
            int skip = (pageIndex.Value - 1) * pageSize.Value;

            result = result.Skip(skip).Take(pageSize.Value);
        }
        #endregion



        return result;
    }


    private static bool ValidDtoProperty<TResult>(string? orderBy)
    {
        return !string.IsNullOrWhiteSpace(orderBy)
                && typeof(TResult).GetProperty(orderBy, BindingFlags.IgnoreCase
                | BindingFlags.Public | BindingFlags.Instance) is not null;
    }


    private static bool ValidProperty<T>(string? orderBy) where T : BaseEntity
    {
        return !string.IsNullOrWhiteSpace(orderBy)
                && typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase
                | BindingFlags.Public | BindingFlags.Instance) is not null;
    }


}
