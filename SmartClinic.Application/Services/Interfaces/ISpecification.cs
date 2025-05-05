using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Interfaces;
public interface ISpecification<T> where T : BaseEntity
{
    Expression<Func<T, bool>>? Criteria { get; }
}
