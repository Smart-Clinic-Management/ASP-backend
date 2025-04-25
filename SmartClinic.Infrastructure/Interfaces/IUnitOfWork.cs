namespace SmartClinic.Infrastructure.Interfaces;
public interface IUnitOfWork : IDisposable
{
    TEntity Repository<TEntity>();

    Task<bool> SaveChangesAsync();

}
