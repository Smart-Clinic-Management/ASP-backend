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
     [Required] string firstName,
    [Required] string lastName,
    string? image,
   [Required] List<string> Specializations
 );
}
