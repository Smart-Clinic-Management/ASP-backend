using SmartClinic.Application.Features.Doctors.Query.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorService
{
    //Task<Response<CreateDoctorResponse>> CreateDoctor(CreateDoctorRequest newDoctorUser);
    //Task<Response<UpdateDoctorResponse>> UpdateDoctorAsync(int doctorId, UpdateDoctorRequest request);

    Task<Response<GetDoctorByIdResponse?>> GetDoctorByIdAsync(int doctorId);

    Task<Response<Pagination<GetAllDoctorsResponse>>> GetAllDoctorsAsync(GetAllDoctorsParams allDoctorsParams);

    //Task<Response<SoftDeleteDoctorResponse>> SoftDeleteDoctorAsync(int doctorId);

    //Task<Response<GetDoctorWithAvailableAppointment>> GetDoctorWithAvailableSchedule(int id, DateOnly startDate);
}
