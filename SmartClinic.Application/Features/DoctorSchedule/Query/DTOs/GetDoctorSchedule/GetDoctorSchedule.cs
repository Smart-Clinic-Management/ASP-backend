using System;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule
{
    public record GetDoctorSchedule(
        [Required] int DoctorId,
        [Required] DayOfWeek DayOfWeek,
        [Required] TimeOnly StartTime,
        [Required] TimeOnly EndTime,
        [Required] int SlotDuration
    );
}
