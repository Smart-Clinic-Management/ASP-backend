using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.DTOs
{
    public record GetAllPatientsResponse
 (
     int Id,
     string FirstName,
     string LastName,
     string? MedicalHistory,
     List<PatientAppointmentDto> Appointments
 );
}
