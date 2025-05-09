using SmartClinic.Application.Features.Appointments.Query.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;

namespace SmartClinic.Application.Services.Interfaces;

public interface IAppointmentService
{
    Task<Response<Pagination<AllAppointmentsResponseDto>>> ListAllAppointmentsAsync(AllAppointmentsParams appointmentsParams);

    Task<Response<Pagination<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, GetDoctorAppointmentsParams appointmentParams);

    Task<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, GetPatientAppointmentsParams appointmentParams);

    Task<Response<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto, int patientId);
}
