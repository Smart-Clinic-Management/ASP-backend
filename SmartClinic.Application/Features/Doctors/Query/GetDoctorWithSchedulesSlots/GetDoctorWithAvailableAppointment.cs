namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
public record GetDoctorWithSchedulesSlotsResponse(
         string FirstName,
         string? LastName,
         string Email,
         string PhoneNumber,
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

