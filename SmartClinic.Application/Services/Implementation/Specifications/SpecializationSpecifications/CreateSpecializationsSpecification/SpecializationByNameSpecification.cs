using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.CreateSpecializationsSpecification;

public class SpecializationByNameSpecification : BaseSpecification<Specialization>
{
    public SpecializationByNameSpecification(string name)
        : base(x => x.Name.Contains(name))
    {
        Criteria = x => x.Name == name;
        Includes = [];
        IsPagingEnabled = false;
        Take = 1;
        Skip = 0;
        IsDistinct = true;
    }

    public Expression<Func<Specialization, bool>> Criteria { get; }
    public string? OrderBy { get; }
    public string? OrderByDescending { get; }
    public List<Expression<Func<Specialization, object>>> Includes { get; }
    public List<string> IncludeStrings { get; } = [];
    public bool IsDistinct { get; }
    public int Take { get; }
    public int Skip { get; }
    public bool IsPagingEnabled { get; }

    public bool IsNoTracking => throw new NotImplementedException();

    public IQueryable<Specialization> ApplyCriteria(IQueryable<Specialization> query)
    {
        if (Criteria != null)
            query = query.Where(Criteria);

        if (IsPagingEnabled)
            query = query.Skip(Skip).Take(Take);

        return query;
    }
}