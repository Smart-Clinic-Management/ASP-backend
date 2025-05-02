using SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.PatientAppointments;

namespace SmartClinic.Application.Services.Interfaces;

public interface IAppointmentService
{
    Task<Response<List<AppointmentResponseDto>>> ListAllAppointmentsAsync(int pageSize = 20, int pageIndex = 1);

    Task<Response<List<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, int pageSize = 20, int pageIndex = 1);

    Task<Response<List<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, int pageSize = 20, int pageIndex = 1);

    Task<Response<string>> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
}
