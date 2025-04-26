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
    [EmailAddress] string Email,
    [Required] string PhoneNumber,
    [Required] DateOnly BirthDate,
    [Required] string Address,
    string? Description,
    int? WaitingTime,
    string? ProfileImage,
    int? SlotDuration
// [Required] List<int> Specializations
);
}
