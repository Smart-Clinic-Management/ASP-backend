using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Application.Services.Implementation;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDoctorService, DoctorService>();


        return services;
    }
}
