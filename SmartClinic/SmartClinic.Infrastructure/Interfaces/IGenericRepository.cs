using System.Linq.Expressions;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastructure.Interfaces;

public interface IGenericRepository<T>
    where T : BaseEntity
{

    Task<T?> GetByIdAsync(int id);

    Task<T?> GetByIdAsync(int id, bool withTracking = true, params string[] includes);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>>? criteria = null, bool withTracking = true, params string[] includes);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, bool>>? criteria = null,
        int pageSize = 20, int pageIndex = 1, bool withTracking = true, params string[] includes);



}
