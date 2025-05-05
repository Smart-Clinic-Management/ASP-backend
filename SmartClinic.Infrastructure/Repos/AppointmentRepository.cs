using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Infrastructure.Repos;
public class AppointmentRepository(ApplicationDbContext context)
    : GenericRepository<Appointment>(context),
    IAppointment
{
    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await base.GetSingleAsync(x => x.Id == id);
    }

    public async Task<Appointment?> GetByIdNoTrackingAsync(int id)
    {
        return await base.GetSingleAsync(x => x.Id == id, false);
    }

    public async Task<Appointment?> GetByIdWithIncludesAsync(int id)
    {
        return await base.GetSingleAsync(x => x.Id == id, true,
            nameof(Appointment.Specialization), nameof(Appointment.Doctor), nameof(Appointment.Patient));
    }

    public async Task<Appointment?> GetByIdWithIncludesNoTrackingAsync(int id)
    {
        return await base.GetSingleAsync(x => x.Id == id, false,
           nameof(Appointment.Specialization), nameof(Appointment.Doctor), nameof(Appointment.Patient));
    }


    //for admin
    public async Task<IEnumerable<Appointment>> ListPatientAppointmentsAsync(int patientId, int pageSize = 20, int pageIndex = 1)
    {
        return await base.ListAllAsync(x => x.Patient.IsActive && x.PatientId == patientId,
            pageSize: pageSize, pageIndex: pageIndex, false,
            $"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}",
            $"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}",
            nameof(Appointment.Specialization));
    }


    //for admin
    public async Task<IEnumerable<Appointment>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1)
    {
        return await base.ListAllAsync(x => x.Doctor.IsActive && x.DoctorId == doctorId,
            pageSize: pageSize, pageIndex: pageIndex, false,
            $"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}",
            $"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}",
            nameof(Appointment.Specialization));
    }


    //for admin
    public async Task<IEnumerable<Appointment>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1)
    {
        return await base.ListAllAsync(
            criteria: null,
             pageSize: pageSize,
             pageIndex: pageIndex,
             withTracking: false,
             $"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}",
             $"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}",
             nameof(Appointment.Specialization));
    }

}
