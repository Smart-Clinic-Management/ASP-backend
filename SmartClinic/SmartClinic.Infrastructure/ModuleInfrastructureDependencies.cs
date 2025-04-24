using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Infrastructure.Interfaces;

namespace SmartClinic.Infrastructure;
public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
