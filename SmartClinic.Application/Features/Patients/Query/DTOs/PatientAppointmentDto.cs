using SmartClinic.Domain.Entities.AppointmentAggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.DTOs
{
    public record PatientAppointmentDto
 (
     DateOnly AppointmentDate,
     AppointmentDuration Duration,
     AppointmentStatus Status,
     DoctorInAppointmentDto Doctor
 );
}
