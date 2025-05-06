namespace SmartClinic.Application.Features.Doctors.Command.UpdateDoctor;
public class UpdateDoctorResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int? WaitingTime { get; set; }

}
