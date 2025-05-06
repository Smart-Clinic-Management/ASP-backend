namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithAvailableAppointment;
public record GetDoctorWithAvailableAppointment(
         string FirstName,
         string? LastName,
         string UserEmail,
         string UserPhoneNumber,
        int Age,
        string Address,
        string? Description,
        int? WaitingTime,
        string Image,
        int SpecializationId,
        string Specialization,
        List<AvailableSchedule> AvailableSchedule
    );



public class AvailableSchedule
{
    public string Day { get; set; }
    public bool IsAvailable { get; set; }
    public List<Slot> Slots { get; set; } = [];

}

public record Slot(TimeOnly Time, bool IsAvailable);

