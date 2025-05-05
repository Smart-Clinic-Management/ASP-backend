namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IUnitOfWork : IDisposable
{
    TEntity Repository<TEntity>();

    Task<bool> SaveChangesAsync();

}
