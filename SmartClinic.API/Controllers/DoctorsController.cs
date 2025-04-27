using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor;
using SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _doctorService.GetDoctorByIdAsync(id));
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromForm] UpdateDoctorRequest request)
        {
            var response = await _doctorService.UpdateDoctorAsync(id, request);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDoctors(int pageSize = 20, int pageIndex = 1)
        {
            return Ok(await _doctorService.GetAllDoctorsAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteDoctor(int id)
        {
            return Ok(await _doctorService.SoftDeleteDoctorAsync(id));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
        {
            return Ok(await _doctorService.CreateDoctor(request));
        }


    }
}
