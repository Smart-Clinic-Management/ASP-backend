namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);

    Task<T?> GetByIdNoTrackingAsync(int id);

    Task<T?> GetByIdWithIncludesAsync(int id);

    Task<T?> GetByIdWithIncludesNoTrackingAsync(int id);

    Task<IEnumerable<T>> ListAsync(int pageSize = 20, int pageIndex = 1);

    Task<IEnumerable<T>> ListNoTrackingAsync(int pageSize = 20, int pageIndex = 1);

    Task<IEnumerable<TDto>> ListNoTrackingAsync<TDto>(int pageSize = 20, int pageIndex = 1, string? orderBy = null,
        bool descending = false, bool isDistinct = false);

    Task<bool> SoftDeleteAsync(int id);

    Task<int> CountAsync();

    Task<bool> ExistsAsync(int id);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}
