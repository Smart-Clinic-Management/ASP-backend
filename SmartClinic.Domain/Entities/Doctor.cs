namespace SmartClinic.Domain.Entities;
public class Doctor : BaseEntity
{


    [Column(TypeName = "VARCHAR(500)")]
    public string? Description { get; set; }

    public int? WaitingTime { get; set; }

    public int SpecializationId { get; set; }

    public bool IsActive { get; set; } = true;

    public AppUser User { get; set; } = null!;

    [ForeignKey(nameof(SpecializationId))]
    public Specialization Specialization { get; set; } = null!;

    public IEnumerable<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();

    public IEnumerable<Appointment> Appointments { get; set; } = new HashSet<Appointment>();


}
