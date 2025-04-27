using SmartClinic.Application.Services.Implementation.FileHandlerService;
using SmartClinic.Application.Services.Implementation.Profile;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ResponseHandler>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<UserManager<AppUser>>();
        services.AddScoped<SignInManager<AppUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();
        services.AddHttpContextAccessor();


        services.AddScoped<IFetchProfile, FetchPatientProfile>();
        services.AddScoped<IFetchProfile, FetchDoctorProfile>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IFileHandlerService, FileHandler>();
        return services;
    }
}
