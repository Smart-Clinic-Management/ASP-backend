using SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.PatientAppointments;

namespace SmartClinic.Application.Features.Appointments.Mapper;

public static class AppointmentMappingExtension
{
    public static string GetFullName(this AppUser user)
    {
        return $"{user.FirstName} {user.LastName ?? string.Empty}".Trim();
    }

    public static AppointmentResponseDto ToDto(this Appointment appointment)
    {
        var doctorFullName = appointment.Doctor.User.GetFullName();
        var patientFullName = appointment.Patient.User.GetFullName();

        return new AppointmentResponseDto(
            Id: appointment.Id,
            DoctorId: appointment.DoctorId,
            DoctorFullName: doctorFullName,
            PatientId: appointment.PatientId,
            PatientFullName: patientFullName,
            SpecializationName: appointment.Specialization.Name,
            AppointmentDate: appointment.AppointmentDate,
            StartTime: appointment.Duration.StartTime,
            EndTime: appointment.Duration.EndTime,
            Status: appointment.Status.ToString()
        );
    }


    public static DoctorWithAppointmentsResponseDto ToDoctorDto(this Appointment appointment)
    {
        return new DoctorWithAppointmentsResponseDto(
            Id: appointment.Id,
            PatientId: appointment.PatientId,
            PatientFullName: appointment.Patient.User.GetFullName(),
            AppointmentDate: appointment.AppointmentDate,
            StartTime: appointment.Duration.StartTime,
            EndTime: appointment.Duration.EndTime,
            Status: appointment.Status.ToString()
        );
    }
    public static PatientAppointmentsWithDoctorDetailsDto ToPatientDto(this Appointment appointment)
    {
        return new PatientAppointmentsWithDoctorDetailsDto(
            AppointmentId: appointment.Id,
            DoctorId: appointment.DoctorId,
            DoctorFullName: appointment.Doctor.User.GetFullName(),
            AppointmentDate: appointment.AppointmentDate,
            StartTime: appointment.Duration.StartTime,
            EndTime: appointment.Duration.EndTime,
            Status: appointment.Status.ToString()
        );
    }

    public static Appointment ToEntity(this CreateAppointmentDto appointmentDto, int patientId)
        => new()
        {
            Duration = new(appointmentDto.StartTime,
            appointmentDto.StartTime.AddMinutes(appointmentDto.TimeSlot)),
            AppointmentDate = appointmentDto.AppointmentDate,
            DoctorId = appointmentDto.DoctorId,
            PatientId = patientId,
            SpecializationId = appointmentDto.SpecializationId,
        };
}
