using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;

public record GetDoctorByIdResponse(
    [Required] string firstName,
    [Required] string lastName,
    [EmailAddress] string userEmail,
    [Required] string userPhoneNumber,
     [Required] int age,
    [Required] string address,
    string? description,
    int? waitingTime,
    string? image,
    int? SlotDuration,
    string Specialization
);
