using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.API.Bases;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorScheduleController : AppControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpGet("GetByDoctor/{doctorId}")]
        public async Task<IActionResult> GetScheduleForDoctor(int doctorId)
        {
            var response = await _doctorScheduleService.GetSchedulesForDoctorAsync(doctorId);

            if (response == null || !response.Data.Any())
            {
                return NotFound(new { message = $"No schedules found for doctor {doctorId}" });
            }

            return NewResult(response);
        }

    }
}
