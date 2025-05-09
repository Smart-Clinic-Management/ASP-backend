namespace SmartClinic.Application.Features.Appointments.Query.PatientAppointments;

public record PatientAppointmentsWithDoctorDetailsDto
 (
     int AppointmentId,
     int DoctorId,
     string DoctorFullName,
     DateOnly AppointmentDate,
     TimeOnly StartTime,
     TimeOnly EndTime,
     string Status
 );
