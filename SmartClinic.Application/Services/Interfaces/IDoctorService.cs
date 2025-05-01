using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctorWithAvailableAppointment;

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorService
{
    Task<Response<CreateDoctorResponse>> CreateDoctor(CreateDoctorRequest newDoctorUser);
    Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request);

    Task<Response<GetDoctorByIdResponse>> GetDoctorByIdAsync(int doctorId);

    Task<Response<List<GetAllDoctorsResponse>>> GetAllDoctorsAsync(int pageSize = 20, int pageIndex = 1);

    Task<Response<SoftDeleteDoctorResponse>> SoftDeleteDoctorAsync(int doctorId);

    Task<Response<GetDoctorWithAvailableAppointment>> GetDoctorWithAvailableSchedule(int id, DateOnly startDate);


}
