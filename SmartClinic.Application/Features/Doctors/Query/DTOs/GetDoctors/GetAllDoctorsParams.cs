namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;
public class GetAllDoctorsParams : PagingParams
{
    public string? DoctorName { get; set; }

    public string? Specialization { get; set; }


}
