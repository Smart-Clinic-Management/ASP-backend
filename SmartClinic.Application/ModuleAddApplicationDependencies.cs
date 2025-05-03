using Microsoft.Extensions.Configuration;
using SmartClinic.Application.Models;
using SmartClinic.Application.Services.Implementation.Profile;

namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<ResponseHandler>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorScheduleService, DoctorScheduleServices>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IPatient, PatientRepo>();


        services.Configure<EmailSettings>(configuration.GetRequiredSection("EmailSettings"));

        services.AddScoped<IAuthService, AuthService>();
        services.AddHttpContextAccessor();


        services.AddScoped<IFetchProfile, FetchPatientProfile>();
        services.AddScoped<IFetchProfile, FetchDoctorProfile>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IFileHandlerService, FileHandler>();
        return services;
    }
}