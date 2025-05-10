namespace SmartClinic.Domain.Entities;
public class Patient : BaseEntity
{

    [Column(TypeName = "VARCHAR(255)")]
    public string? MedicalHistory { get; set; }

    public bool IsActive { get; set; } = true;

    public AppUser User { get; set; } = null!;

    public IEnumerable<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

}
