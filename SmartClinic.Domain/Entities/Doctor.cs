using System.ComponentModel.DataAnnotations.Schema;
using SmartClinic.Domain.Entities.AppointmentAggregation;

namespace SmartClinic.Domain.Entities;
public class Doctor : BaseEntity
{


    [Column(TypeName = "VARCHAR(500)")]
    public string? Description { get; set; }

    public int? WaitingTime { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int SpecializationId { get; set; }

    public bool IsActive { get; set; } = true;


    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; } = null!;

    [ForeignKey(nameof(SpecializationId))]
    public Specialization Specialization { get; set; } = null!;

    public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();

    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();


}
