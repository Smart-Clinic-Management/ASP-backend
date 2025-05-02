using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClinic.Application.Features.Patients.Query.DTOs.GetPatient
{
    public record GetDoctorSchedule(
    [Required] int id,
    [Required] string firstName,
    [Required] string lastName,
    [EmailAddress] string userEmail,
    [Required] string userPhoneNumber,
    [Required] byte age,
    [Required] string address,
    string? image,
    string? medicalHistory
);
}
