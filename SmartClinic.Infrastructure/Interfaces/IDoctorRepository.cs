namespace SmartClinic.Infrastructure.Interfaces;
public interface IDoctorRepository : IRepository<Doctor>
{
    Task<bool> ExistsAsync(int doctorId);
    Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate);
}
