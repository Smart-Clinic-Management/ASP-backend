
using SmartClinic.Application.Bases;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IDoctorService
    {

        Task<Response<CreateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request);

        Task<Response<Doctor?>> GetDoctorByIdAsync(int doctorId);
    }
}
