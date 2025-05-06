using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Infrastructure;
public class UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context) : IUnitOfWork

{
    public readonly ConcurrentDictionary<string, Lazy<object>> _repositories = [];

    public void Dispose() => context.Dispose();

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;

    public IGenericRepo<TEntity> Repo<TEntity>()
        where TEntity : BaseEntity
    {
        var key = typeof(TEntity).FullName!;

        var lazy = _repositories.GetOrAdd(key,
           _ => new Lazy<object>(() => serviceProvider.GetRequiredService<IGenericRepo<TEntity>>())
           );

        return (IGenericRepo<TEntity>)lazy.Value;
    }

}

