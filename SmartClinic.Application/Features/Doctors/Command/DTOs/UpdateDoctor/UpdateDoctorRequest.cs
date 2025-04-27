using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
public record UpdateDoctorRequest(
    string? Fname,
    string? Lname,
    [EmailAddress] string? Email,
    IFormFile? Image,
    List<int>? Specialization,
    DateOnly? BirthDate,
    string? Address,
    int? WaitingTime,
    string? Description
);



