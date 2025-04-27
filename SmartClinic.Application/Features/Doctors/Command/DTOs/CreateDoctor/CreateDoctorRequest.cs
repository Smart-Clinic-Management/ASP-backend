using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor
{
    public record CreateDoctorRequest(
     [Required] string Fname,
     string Lname,
     [EmailAddress] string Email,
     [Required] IFormFile Image,  
     [Required] List<int> Specialization,
     DateOnly BirthDate,
     [Required] string Address,
     int? WaitingTime,
     string? Description
 );


}
