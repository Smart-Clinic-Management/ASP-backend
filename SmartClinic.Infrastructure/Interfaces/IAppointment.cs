

namespace SmartClinic.Infrastructure.Interfaces;
public interface IAppointment
{
    Task<IEnumerable<Appointment>> ListPatientAppointmentsAsync(int patientId, int pageSize = 20, int pageIndex = 1);
    Task<IEnumerable<Appointment>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1);
    Task<IEnumerable<Appointment>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1);
}
