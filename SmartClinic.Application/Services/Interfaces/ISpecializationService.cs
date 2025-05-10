namespace SmartClinic.Application.Services.Interfaces;

public interface ISpecializationService
{
    Task<Response<UpdateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest request);
    Task<Response<string>> DeleteSpecializationAsync(int specializationId);

    Task<Response<GetSpecializationByIdResponse?>> GetSpecializationByIdAsync(int SpecializationId);

    Task<Response<Pagination<GetAllSpecializationsResponse>>> GetAllSpecializationsAsync(GetAllSpecializationsParams allSpecializationsParams);

    Task<Response<string>> CreateSpecialization(CreateSpecializationRequest newSpecializationUser);

}
