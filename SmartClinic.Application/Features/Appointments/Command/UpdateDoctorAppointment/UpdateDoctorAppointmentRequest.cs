﻿namespace SmartClinic.Application.Features.Appointments.Command.UpdateDoctorAppointment;
public record UpdateDoctorAppointmentRequest(int AppointmentId, AppointmentStatus Status);
