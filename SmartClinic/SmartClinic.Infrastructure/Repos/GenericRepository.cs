using System.Linq.Expressions;
using SmartClinic.Domain.Entities;
using SmartClinic.Infrastructure.Data;
using SmartClinic.Infrastructure.Interfaces;

namespace SmartClinic.Infrastructure.Repos;

public abstract class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _db = context.Set<T>();


    public async Task AddAsync(T entity) => await _db.AddAsync(entity);

    public void Update(T entity) => _db.Update(entity);

    public void Delete(T entity) => _db.Remove(entity);


    public async Task<IEnumerable<T>> ListAllAsync(Expression<Func<T, bool>>? criteria = null,
        int pageSize = 20, int pageIndex = 1, bool withTracking = true, params string[] includes)
    {
        IQueryable<T> query = _db;

        query = query.GetQuery(criteria, pageSize, pageIndex, withTracking, includes);

        return await query.ToListAsync();
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>>? criteria = null, bool withTracking = true, params string[] includes)
    {
        IQueryable<T> query = _db;

        query = query.GetQuery(criteria, withTracking: withTracking, includes: includes);

        return await query.FirstOrDefaultAsync();
    }
}
