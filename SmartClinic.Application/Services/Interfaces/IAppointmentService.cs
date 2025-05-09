using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;

namespace SmartClinic.Application.Services.Interfaces;

public interface IAppointmentService
{
    //Task<Response<List<AppointmentResponseDto>>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1);

    //Task<Response<List<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1);

    Task<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, GetPatientAppointmentParams appointmentParams);

    Task<Response<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, int patientId);
}
