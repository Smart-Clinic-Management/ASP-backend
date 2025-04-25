using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor
{
    public record GetDoctorByIdResponse(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string PhoneNumber,
    [EmailAddress] string Email,
    [Required] DateOnly BirthDate,
    [Required] string Address,
    string? Description,
    int? WaitingTime,
    [Required] List<int> Specializations
);
}
