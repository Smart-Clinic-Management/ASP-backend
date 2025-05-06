using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SmartClinic.Domain.Entities;

public class AppUser : IdentityUser<int>
{
    [Column(TypeName = "VARCHAR(255)")]
    public required string FirstName { get; set; }


    [Column(TypeName = "VARCHAR(255)")]
    public string? LastName { get; set; }


    [Column(TypeName = "VARCHAR(255)")]
    public required string Address { get; set; }

    public DateOnly BirthDate { get; set; }

    public int Age { get; private set; }

    public bool IsActive { get; set; } = true;


    [Column(TypeName = "VARCHAR(255)")]
    public string? ProfileImage { get; set; }

    public Doctor? Doctor { get; set; }

    public Patient? Patient { get; set; }

}
