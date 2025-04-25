using SmartClinic.Application.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Auth;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDoctorService, DoctorService>();

        services.AddScoped<ResponseHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();

        return services;
    }
}
