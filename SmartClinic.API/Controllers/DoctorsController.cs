using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = await _doctorService.GetDoctorByIdAsync(id);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result.Message);  
            }
            return Ok(result.Data);
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorRequest request)
        {
            var result = await _doctorService.UpdateDoctorAsync(id, request);

            return StatusCode((int)result.StatusCode, result); 
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors(int pageSize = 20, int pageIndex = 1)
        {
            var result = await _doctorService.GetAllDoctorsAsync(pageSize, pageIndex);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result.Message); 
            }
            return Ok(result.Data); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteDoctor(int id)
        {
            var result = await _doctorService.SoftDeleteDoctorAsync(id);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(result.Message);  
            }
            return Ok(result.Data);  
        }
    }
}
