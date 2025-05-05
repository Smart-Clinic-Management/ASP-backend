using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

public interface IGenericRepository<T>
    where T : BaseEntity
{


    Task<T?> GetSingleAsync(Expression<Func<T, bool>>? criteria = null, bool withTracking = true, params string[] includes);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);


    Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, bool>>? criteria = null,
      int pageSize = 20, int pageIndex = 1, string? orderBy = null,
      bool descending = false, bool isDistinct = false,
      params string[] includes);

    Task<IEnumerable<TResult>> ListAllAsync<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>>? criteria = null,
         int? pageSize = null, int? pageIndex = null, string? orderBy = null,
        bool descending = false, bool isDistinct = false) where TResult : IDto, new();


    Task<bool> ExistsAsync(int id);

    Task<int> CountAsync(Expression<Func<T, bool>>? critera = null);

}
