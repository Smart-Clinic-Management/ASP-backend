namespace SmartClinic.Application.Features.Appointments.Query.AllAppointments;

public record AllAppointmentsResponseDto
(
    int AppointmentId,
    int DoctorId,
    string DoctorFullName,
    int PatientId,
    string PatientFullName,
    string SpecializationName,
    DateOnly AppointmentDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Status
);
