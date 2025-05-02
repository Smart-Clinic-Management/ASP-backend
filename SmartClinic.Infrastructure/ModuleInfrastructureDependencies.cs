using Microsoft.Extensions.DependencyInjection;
using SmartClinic.Infrastructure.Repos;

namespace SmartClinic.Infrastructure;
public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPatient, PatientRepo>();

        services.AddScoped<IDoctorRepository, DoctorRepository>();

        services.AddScoped<IAppointment, AppointmentRepository>();

        services.AddScoped<IDoctorSchedule, DoctorScheduleRepository>();



        services.AddScoped<ISpecializaionRepository, SpecializationRepository>();

        return services;
    }
}