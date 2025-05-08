using System.Linq.Dynamic.Core;
using System.Reflection;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.Infrastructure;

public class SpecificationEvaluator<T>
     where T : BaseEntity
{

    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        if (spec.IsNoTracking)
            query = query.AsNoTracking();

        #region Ordering

        #region Asc
        if (ValidProperty(spec.OrderBy))
            query = query.OrderBy(spec.OrderBy!);
        else
            query = query.OrderBy(x => x.Id);
        #endregion

        #region Desc
        if (ValidProperty(spec.OrderByDescending))
            query = query.OrderBy(spec.OrderByDescending!);
        else
            query = query.OrderByDescending(x => x.Id);
        #endregion

        #endregion


        if (spec.IsDistinct)
            query = query.Distinct();

        if (spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        query = spec.Includes.Aggregate(query, (current, include)
            => current.Include(include));

        query = spec.IncludeStrings.Aggregate(query, (current, include)
            => current.Include(include));

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        query = query.AsNoTracking();

        var selectQuery = query.Select(spec.Select);

        #region Ordering

        #region Asc
        if (ValidProperty<TResult>(spec.OrderBy))
            selectQuery = selectQuery.OrderBy(spec.OrderBy!);
        //else
        //    selectQuery = selectQuery.OrderBy();
        #endregion

        #region Desc
        if (ValidProperty(spec.OrderByDescending))
            selectQuery = selectQuery.OrderBy(spec.OrderByDescending!);
        //else
        //    selectQuery = selectQuery.OrderByDescending();
        #endregion

        #endregion

        if (spec.IsDistinct)
            selectQuery = selectQuery?.Distinct();

        if (spec.IsPagingEnabled)
            selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);

        return selectQuery ?? query.Cast<TResult>();
    }

    private static bool ValidProperty<TResult>(string? orderBy)
    {
        return !string.IsNullOrWhiteSpace(orderBy)
                && typeof(TResult).GetProperty(orderBy, BindingFlags.IgnoreCase
                | BindingFlags.Public | BindingFlags.Instance) is not null;
    }


    private static bool ValidProperty(string? orderBy)
    {
        return !string.IsNullOrWhiteSpace(orderBy)
                && typeof(T).GetProperty(orderBy, BindingFlags.IgnoreCase
                | BindingFlags.Public | BindingFlags.Instance) is not null;
    }

}