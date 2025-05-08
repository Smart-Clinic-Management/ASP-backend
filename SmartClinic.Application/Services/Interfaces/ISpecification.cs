using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Interfaces;
public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    string? OrderBy { get; }
    string? OrderByDescending { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    bool IsDistinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    bool IsNoTracking { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>> Select { get; }
}
