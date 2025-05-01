namespace SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments
{
    public record AppointmentResponseDto
    (
        int Id,
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
}
