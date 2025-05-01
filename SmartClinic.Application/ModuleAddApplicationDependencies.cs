using SmartClinic.Application.Services.Implementation.FileHandlerService;
using SmartClinic.Application.Services.Implementation.Profile;
using SmartClinic.Infrastructure.Repos;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<ResponseHandler>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IPatient, PatientRepo>();


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