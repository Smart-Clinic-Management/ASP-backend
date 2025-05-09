namespace SmartClinic.Application.Features.Appointments.Query.AllAppointments;
public class AllAppointmentsParams : PagingParams
{
    public int? DoctorId { get; set; }
    public int? PatientId { get; set; }
}
