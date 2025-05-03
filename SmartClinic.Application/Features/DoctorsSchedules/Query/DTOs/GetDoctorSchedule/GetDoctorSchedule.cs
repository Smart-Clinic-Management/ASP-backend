using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.DoctorSchedule.Query.DTOs.GetDoctorSchedule;

public record GetDoctorSchedule(
    [Required] int DoctorId,
    [Required] string DayOfWeek,
    [Required] TimeOnly StartTime,
    [Required] TimeOnly EndTime,
    [Required] int SlotDuration
);
