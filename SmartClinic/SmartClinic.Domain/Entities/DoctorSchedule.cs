using System.ComponentModel.DataAnnotations.Schema;

namespace SmartClinic.Domain.Entities;
public class DoctorSchedule : BaseEntity
{

    public int DoctorId { get; set; }

    public DaysOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDuration { get; set; }


    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;

}



public enum DaysOfWeek
{
    Sat,
    Sun,
    Mon,
    Tue,
    Wed,
    Thu,
    Fri
}
