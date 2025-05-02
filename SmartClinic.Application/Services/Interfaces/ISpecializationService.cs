using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface ISpecializationService
    {
        Task<Response<CreateSpecializationResponse>> CreateSpecializationAsync(CreateSpecializationRequest request);
        Task<Response<UpdateSpecializationResponse>> UpdateSpecializationAsync(int specializationId, UpdateSpecializationRequest request);
        Task<Response<CreateSpecializationResponse?>> GetSpecializationByIdAsync(int specializationId);
        Task<Response<List<CreateSpecializationResponse>>> GetAllSpecializationsAsync(); 
        Task<Response<string>> DeleteSpecializationAsync(int specializationId);
    }
}
