

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorService
{
    Task<Response<string>> CreateDoctor(CreateDoctorRequest newDoctorUser);

    Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request);

    Task<Response<GetDoctorByIdResponse?>> GetDoctorByIdAsync(int doctorId);

    Task<Response<Pagination<GetAllDoctorsResponse>>> GetAllDoctorsAsync(GetAllDoctorsParams allDoctorsParams);

    Task<Response<string>> DeleteById(int id);

    Task<Response<GetDoctorWithSchedulesSlotsResponse>> GetDoctorWithSchedulesSlots(GetDoctorWithSchedulesSlotsParams schedulesSlotsParams);
}
