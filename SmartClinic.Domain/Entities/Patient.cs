using System.ComponentModel.DataAnnotations.Schema;
using SmartClinic.Domain.Entities.AppointmentAggregation;

namespace SmartClinic.Domain.Entities;
public class Patient : BaseEntity
{

    [Column(TypeName = "VARCHAR(255)")]
    public string? MedicalHistory { get; set; }

    public bool IsActive { get; set; } = true;

    public string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; } = null!;

    public ICollection<Appointment> Appointments { get; set; } = [];

}
