using System.ComponentModel.DataAnnotations.Schema;
using SmartClinic.Domain.Entities.AppointmentAggregation;

namespace SmartClinic.Domain.Entities;
public class Specialization : BaseEntity
{

    [Column(TypeName = "VARCHAR(150)")]
    public required string Name { get; set; }

    [Column(TypeName = "VARCHAR(255)")]
    public string? Description { get; set; }

    [Column(TypeName = "VARCHAR(150)")]
    public string? Image { get; set; }

    public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}
