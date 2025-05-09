using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Implementation.Specifications;
public abstract class BaseSpecification<T>(Expression<Func<T, bool>>? criteria = null) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria => criteria;

    public string? OrderBy { get; private set; }

    public string? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public List<string> IncludeStrings { get; } = [];

    public bool IsNoTracking { get; private set; }

    protected void AsNoTracking() => IsNoTracking = true;

    protected void AddOrderBy(string? orderBy) => OrderBy = orderBy;

    protected void AddOrderByDescending(string? orderByDesc)
        => OrderByDescending = orderByDesc;

    protected void AddDistinct() => IsDistinct = true;

    protected void AddPagination(int pageIndex, int pageSize)
    {
        Skip = (pageIndex - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (criteria is not null)
            query = query.Where(criteria);
        return query;
    }

    protected void AddInclude(params Expression<Func<T, object>>[] includeExpressions) => Includes.AddRange(includeExpressions);

    protected void AddInclude(params string[] includeStrings) => IncludeStrings.AddRange(includeStrings);


}

public abstract class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria = null)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    public Expression<Func<T, TResult>> Select { get; private set; } = null!;

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression) => Select = selectExpression;


}
