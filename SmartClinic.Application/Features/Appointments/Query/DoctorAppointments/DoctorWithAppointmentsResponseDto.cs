namespace SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;

public record DoctorWithAppointmentsResponseDto
(
    int Id,
    int PatientId,
    string PatientFullName,
    DateOnly AppointmentDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
      string Status
);
