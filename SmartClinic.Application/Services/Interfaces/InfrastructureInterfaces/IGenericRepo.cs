namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

public interface IGenericRepo<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);

    Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec);

    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec); 

    Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec);

    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<bool> Exists(int id);

    Task<bool> Exists(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);

  
}
