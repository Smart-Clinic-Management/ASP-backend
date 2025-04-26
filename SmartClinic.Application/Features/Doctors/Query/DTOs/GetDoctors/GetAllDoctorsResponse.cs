using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors
{
    public record GetAllDoctorsResponse(
     [Required] string FirstName,
    [Required] string LastName,
    string? ProfileImage,
   [Required] List<string> Specializations
 );
}
