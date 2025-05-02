using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.Bases;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecializationController(ISpecializationService _specializationService) : AppControllerBase
{
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateSpecialization([FromForm] CreateSpecializationRequest request, IFormFile image)
    {
        var response = await _specializationService.CreateSpecializationAsync(request);
        return NewResult(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await _specializationService.GetSpecializationByIdAsync(id));
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllSpecializations()
    {
        var response = await _specializationService.GetAllSpecializationsAsync();
        return NewResult(response);
    }
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateSpecialization(int id, [FromForm] UpdateSpecializationRequest request)
    {
        var response = await _specializationService.UpdateSpecializationAsync(id, request);
        return NewResult(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteSpecialization(int id)
    {
        var response = await _specializationService.DeleteSpecializationAsync(id);
        return NewResult(response);
    }

}
