using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Appointments.Query.DTOs.DoctorAppointments
{
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

}
