namespace SmartClinic.Application.Features.Doctors.Query.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsParams
{
    public int DoctorId { get; set; }
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}