namespace SmartClinic.Application.Services.Interfaces;

public interface IAppointmentService
{
    Task<Response<Pagination<AllAppointmentsResponseDto>>> ListAllAppointmentsAsync(AllAppointmentsParams appointmentsParams);

    Task<Response<Pagination<DoctorWithAppointmentsResponseDto>>> ListDoctorAppointmentsAsync(int doctorId, GetDoctorAppointmentsParams appointmentParams);

    Task<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>> ListPatientAppointmentsAsync(int patientId, GetPatientAppointmentsParams appointmentParams);

    Task<Response<string>> CreateAppointmentAsync(CreateAppointmentRequest appointmentDto, MailData receiverData);

    Task<Response<string>> UpdateDoctorAppointmentAsync(MailData doctorData, UpdateAppointmentRequest updateDoctorAppointment);

    Task<Response<string>> CancelPatientAppointmentAsync(MailData patientData, int appointmentId);
}
