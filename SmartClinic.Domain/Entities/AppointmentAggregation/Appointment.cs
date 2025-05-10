namespace SmartClinic.Domain.Entities.AppointmentAggregation;
public class Appointment : BaseEntity
{

    public int DoctorId { get; set; }

    public int PatientId { get; set; }


    public int SpecializationId { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public required AppointmentDuration Duration { get; set; }

    public AppointmentStatus Status { get; set; }


    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;


    [ForeignKey(nameof(PatientId))]
    public Patient Patient { get; set; } = null!;


    [ForeignKey(nameof(SpecializationId))]
    public Specialization Specialization { get; set; } = null!;
}
