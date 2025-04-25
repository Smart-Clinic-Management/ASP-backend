using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Infrastructure.Repos;

namespace SmartClinic.Infrastructure;
public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDoctorRepository, DoctorRepository>();

        return services;
    }
}
