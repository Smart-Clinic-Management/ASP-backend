namespace SmartClinic.Application.Features.Profile.Command;

public class BaseProfile
{
    public int? Id { get; set; }
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? ProfileImage { get; set; } = null!;

}
