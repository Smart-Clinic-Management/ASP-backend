namespace SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;
public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> GetDoctorWithSpecificScheduleAsync(int doctorId, DateOnly appointmentDate, TimeOnly startTime);
    Task<Doctor?> GetWithAppointmentsAsync(int id, DateOnly startDate);
    Task<bool> IsValidDoctorSpecialization(int specializationId, int doctorId);
    Task<IEnumerable<GetAllDoctorsResponse>> ListNoTrackingAsync(GetAllDoctorsParams doctorsParams,
        ISpecification<Doctor> specification);
    Task<int> CountAsync(ISpecification<Doctor> specification);

}
