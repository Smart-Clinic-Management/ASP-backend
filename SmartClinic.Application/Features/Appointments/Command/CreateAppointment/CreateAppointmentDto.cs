namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public record CreateAppointmentDto
    (
    int DoctorId,
    int PatientId,
    int SpecializationId,
    DateOnly AppointmentDate,
    TimeOnly StartTime

    );