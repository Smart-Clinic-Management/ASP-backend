using SmartClinic.Application.Services.FileHandlerService;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<FileHandler>();
        services.AddScoped<ResponseHandler>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddHttpContextAccessor();

        return services;
    }
}
