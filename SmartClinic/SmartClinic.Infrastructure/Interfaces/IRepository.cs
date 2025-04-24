using SmartClinic.Domain.Entities;

namespace SmartClinic.Infrastructure.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);

    Task<T?> GetByIdNoTrackingAsync(int id);

    Task<T?> GetByIdWithIncludesAsync(int id);

    Task<T?> GetByIdWithIncludesNoTrackingAsync(int id);

    Task<IEnumerable<T>> LisTAsync(int pageSize = 20, int pageIndex = 1);

    Task<IEnumerable<T>> LisTNoTrackingAsync(int pageSize = 20, int pageIndex = 1);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}
