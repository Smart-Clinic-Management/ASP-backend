namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IUnitOfWork : IDisposable
{
    IGenericRepo<TEntity> Repo<TEntity>() where TEntity : BaseEntity;

    Task<bool> SaveChangesAsync();

}
