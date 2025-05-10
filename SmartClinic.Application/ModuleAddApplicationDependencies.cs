namespace SmartClinic.Application;
public static class ModuleAddApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<ResponseHandler>();
        //services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorScheduleService, DoctorScheduleServices>();
        services.AddScoped<IAppointmentService, AppointmentService>();




        services.Configure<GeminiApiSettings>(configuration.GetSection("GeminiApi"));
        services.AddScoped<IGeminiService, GeminiService>();
        services.AddHttpClient();




        services.Configure<EmailSettings>(configuration.GetRequiredSection("EmailSettings"));

        services.AddScoped<IAuthService, AuthService>();
        services.AddHttpContextAccessor();


        services.AddScoped<IFetchProfile, FetchPatientProfile>();
        services.AddScoped<IFetchProfile, FetchDoctorProfile>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IFileHandlerService, FileHandler>();

        services.AddTransient(typeof(IPagedCreator<>), typeof(PagedCreator<>));



        return services;
    }
}