using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Specializations.Command.DTOs.UpdateSpecialization;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController(ISpecializationService _specializationService) : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateSpecialization([FromForm] CreateSpecializationRequest request  , IFormFile image)
        {
            var response = await _specializationService.CreateSpecializationAsync(request);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _specializationService.GetSpecializationByIdAsync(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllSpecializations()
        {
            var response = await _specializationService.GetAllSpecializationsAsync();
            return Ok(response);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, [FromBody] UpdateSpecializationRequest request)
        {
            var response = await _specializationService.UpdateSpecializationAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var response = await _specializationService.DeleteSpecializationAsync(id);
            return Ok(response);
        }
      
    }
}
