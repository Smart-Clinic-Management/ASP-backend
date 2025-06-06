﻿namespace SmartClinic.Application.Features.Appointments.Mapper;

public static class AppointmentMappingExtension
{
    public static string GetFullName(this AppUser user)
    {
        return $"{user.FirstName} {user.LastName ?? string.Empty}".Trim();
    }

    public static Pagination<PatientAppointmentsWithDoctorDetailsDto> ToPatientAppointmentsPaginatedDto(this Pagination<Appointment> appointments)
    {
        return new Pagination<PatientAppointmentsWithDoctorDetailsDto>
        (
            appointments.PageIndex,
            appointments.PageSize,
            appointments.Total,
            appointments.Data.Select(x => x.ToPatientAppointmentsDto())
            );
    }

    public static Pagination<AllAppointmentsResponseDto> ToAllAppointmentsPaginatedDto(this Pagination<Appointment> appointments)
    {
        return new Pagination<AllAppointmentsResponseDto>
        (
            appointments.PageIndex,
            appointments.PageSize,
            appointments.Total,
            appointments.Data.Select(x => x.ToAllAppointmentsDto())
            );
    }

    public static Pagination<DoctorWithAppointmentsResponseDto> ToDoctorAppointmentsPaginatedDto(this Pagination<Appointment> appointments)
    {
        return new Pagination<DoctorWithAppointmentsResponseDto>
        (
            appointments.PageIndex,
            appointments.PageSize,
            appointments.Total,
            appointments.Data.Select(x => x.ToDoctorAppointmentsDto())
            );
    }

    public static DoctorWithAppointmentsResponseDto ToDoctorAppointmentsDto(this Appointment appointment)
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

    public static AllAppointmentsResponseDto ToAllAppointmentsDto(this Appointment appointment)
    {
        return new AllAppointmentsResponseDto(
            AppointmentId: appointment.Id,
            PatientId: appointment.PatientId,
            DoctorId: appointment.DoctorId,
            PatientFullName: appointment.Patient.User.GetFullName(),
            DoctorFullName: appointment.Doctor.User.GetFullName(),
            AppointmentDate: appointment.AppointmentDate,
            StartTime: appointment.Duration.StartTime,
            EndTime: appointment.Duration.EndTime,
            Status: appointment.Status.ToString(),
            SpecializationName: appointment.Specialization.Name
        );
    }
    public static PatientAppointmentsWithDoctorDetailsDto ToPatientAppointmentsDto(this Appointment appointment)
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

    public static Appointment ToEntity(this CreateAppointmentRequest appointmentDto, int patientId, int timeSlot)
        => new()
        {
            Duration = new(appointmentDto.StartTime,
            appointmentDto.StartTime.AddMinutes(timeSlot)),
            AppointmentDate = appointmentDto.AppointmentDate.ToDate(),
            DoctorId = appointmentDto.DoctorId,
            PatientId = patientId,
            SpecializationId = appointmentDto.SpecializationId,
        };
}
