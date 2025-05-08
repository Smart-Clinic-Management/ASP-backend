using SmartClinic.Application.Features.Doctors.Query.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;
using SmartClinic.Application.Features.Specializations.Query.GetSpecialization;
using SmartClinic.Application.Features.Specializations.Query.GetSpecializations;

namespace SmartClinic.Application.Services.Interfaces;

public interface ISpecializationService
{
    //Task<Response<CreateSpecializationResponse>> CreateSpecializationAsync(CreateSpecializationRequest request);
    //Task<Response<UpdateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest request);
    //Task<Response<CreateSpecializationResponse?>> GetSpecializationByIdAsync(int specializationId);
    //Task<Response<List<CreateSpecializationResponse>>> GetAllSpecializationsAsync();
    //Task<Response<string>> DeleteSpecializationAsync(int specializationId);

    Task<Response<GetSpecializationByIdResponse?>> GetSpecializationByIdAsync(int SpecializationId);

    Task<Response<Pagination<GetAllSpecializationsResponse>>> GetAllSpecializationsAsync(GetAllSpecializationsParams allSpecializationsParams);
}
