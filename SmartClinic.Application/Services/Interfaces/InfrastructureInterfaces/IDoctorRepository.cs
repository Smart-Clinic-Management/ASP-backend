namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IDoctorRepository : IRepository<Doctor>
{
    Task<bool> ExistsAsync(int doctorId);
    Task<Doctor?> GetDoctorWithSpecificScheduleAsync(int doctorId, DateOnly appointmentDate, TimeOnly startTime);
    Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate);
    Task<bool> IsValidDoctorSpecialization(int specializationId, int doctorId);
}
