namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepo<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

    Task<bool> SaveChangesAsync();

}
