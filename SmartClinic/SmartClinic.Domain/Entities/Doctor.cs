using System.ComponentModel.DataAnnotations.Schema;
using SmartClinic.Domain.Entities.AppointmentAggregation;

namespace SmartClinic.Domain.Entities;
public class Doctor : BaseEntity
{


    [Column(TypeName = "VARCHAHR(500)")]
    public string? Description { get; set; }

    public int? WaitingTime { get; set; }

    public required string UserId { get; set; }


    [ForeignKey(nameof(UserId))]
    public AppUser User { get; set; } = null!;

    public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();

    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();

    public ICollection<Specialization> Specializations { get; set; } = new HashSet<Specialization>();

}
