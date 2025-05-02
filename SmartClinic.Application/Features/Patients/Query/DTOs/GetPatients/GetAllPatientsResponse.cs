using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients
{
    public record GetAllPatientsResponse(
    [Required] int id,
    [Required] string firstName,
    [Required] string lastName,
    [Required] string userPhoneNumber,
    [Required] byte age
);
}
