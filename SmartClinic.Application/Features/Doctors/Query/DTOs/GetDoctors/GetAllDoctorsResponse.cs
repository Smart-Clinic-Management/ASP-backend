using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors
{
    public record GetAllDoctorsResponse(
     [Required] int Id,
     [Required] string FirstName,
     [Required] string LastName,
     [Required] string PhoneNumber,
     [EmailAddress] string Email,
     [Required] List<int> Specializations
 );
}
