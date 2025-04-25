using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request);

        Task<Response<GetDoctorByIdResponse>> GetDoctorByIdAsync(int doctorId);

        Task<Response<List<GetAllDoctorsResponse>>> GetAllDoctorsAsync(int pageSize = 20, int pageIndex = 1);  // تم تعديل نوع العودة ليتناسب مع GetAllDoctorsResponse
    }
}
