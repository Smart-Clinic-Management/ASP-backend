namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsParams
{
    public int DoctorId { get; set; }
    public string StartDate { get; set; } = null!;
}