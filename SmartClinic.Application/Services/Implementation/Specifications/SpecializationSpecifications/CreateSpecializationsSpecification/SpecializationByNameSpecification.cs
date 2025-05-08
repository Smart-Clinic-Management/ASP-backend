using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.CreateSpecializationsSpecification
{
    public class SpecializationByNameSpecification : ISpecification<Specialization>
    {
        public SpecializationByNameSpecification(string name)
        {
            Criteria = x => x.Name == name;
            Includes = new List<Expression<Func<Specialization, object>>>();
            IsPagingEnabled = false;
            Take = 1;
            Skip = 0;
            IsDistinct = true;
        }

        public Expression<Func<Specialization, bool>> Criteria { get; }
        public string? OrderBy { get; }
        public string? OrderByDescending { get; }
        public List<Expression<Func<Specialization, object>>> Includes { get; }
        public List<string> IncludeStrings { get; } = new List<string>();
        public bool IsDistinct { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagingEnabled { get; }

        public IQueryable<Specialization> ApplyCriteria(IQueryable<Specialization> query)
        {
            if (Criteria != null)
                query = query.Where(Criteria);

            if (IsPagingEnabled)
                query = query.Skip(Skip).Take(Take);

            return query;
        }
    }
}
