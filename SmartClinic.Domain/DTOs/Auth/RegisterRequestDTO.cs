namespace SmartClinic.Domain.DTOs.Auth;

public class RegisterRequestDTO
{
    [Required]
    [MinLength(3)]
    public string Firstname { get; set; } = null!;

    [MinLength(3)]
    public string? Lastname { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    public IFormFile? Image { get; set; } // Corrected type from IFormF to IFormFile  
}
