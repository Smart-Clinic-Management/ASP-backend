namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public record CreateAppointmentDto
(
    int DoctorId,
    int PatientId,
    DateOnly AppointmentDate,
    TimeOnly StartTime

    );