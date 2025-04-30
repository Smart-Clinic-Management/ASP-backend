using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor
{
    public record GetDoctorByIdResponse(
        [Required] string firstName,
        [Required] string lastName,
        [EmailAddress] string userEmail,
        [Required] string userPhoneNumber,
         [Required] byte age,
        [Required] string address,
        string? description,
        int? waitingTime,
        string? image,
        int? SlotDuration,
        List<string> Specializations
    );
}
