namespace SmartClinic.Application.Features.Patients.Query.GetPatients;

public class GetAllPatientsParams : PagingParams
{
    public string? PatientName { get; set; }
}
