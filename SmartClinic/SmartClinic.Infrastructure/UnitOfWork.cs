using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Infrastructure.Data;
using SmartClinic.Infrastructure.Interfaces;

namespace SmartClinic.Infrastructure;
public class UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext context) : IUnitOfWork

{
    public readonly ConcurrentDictionary<string, Lazy<object>> _repositories = [];

    public void Dispose() => context.Dispose();

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;

    public TEntity Repository<TEntity>()
    {
        var key = typeof(TEntity).FullName!;

        var lazy = _repositories.GetOrAdd(key,
           _ => new Lazy<object>(() => serviceProvider.GetRequiredService<TEntity>())
           );

        return (TEntity)lazy.Value;
    }

}

