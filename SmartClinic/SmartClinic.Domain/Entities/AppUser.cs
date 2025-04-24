using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SmartClinic.Domain.Entities;

public class AppUser : IdentityUser
{
    [Column(TypeName = "VARCHAHR(255)")]
    public required string FirstName { get; set; }


    [Column(TypeName = "VARCHAHR(255)")]
    public string? LastName { get; set; }


    [Column(TypeName = "VARCHAHR(255)")]
    public required string Address { get; set; }


    public DateOnly BirthDate { get; set; }


    [Column(TypeName = "VARCHAHR(255)")]
    public string? ProfileImage { get; set; }

    public Doctor? Doctor { get; set; }

    public Patient? Patient { get; set; }

}
