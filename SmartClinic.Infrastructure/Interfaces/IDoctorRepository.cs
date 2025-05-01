namespace SmartClinic.Infrastructure.Interfaces;
public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate);
}
