
namespace SmartClinic.Application.Services.Interfaces;


public interface IPatientService
{
    Task<Response<Pagination<GetAllPatientsResponse>>> GetAllPatientsAsync(GetAllPatientsParams allPatientsParams);

    Task<Response<GetPatientByIdResponse?>> GetPatientByIdAsync(int PatientId);
}
