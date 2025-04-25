using SmartClinic.Application.Features.FileHandlerService;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<FileHandler>();
        services.AddScoped<ResponseHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();

        return services;
    }
}
