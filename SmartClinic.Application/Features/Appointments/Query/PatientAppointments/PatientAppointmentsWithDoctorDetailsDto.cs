using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Appointments.Query.PatientAppointments
{
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
}
