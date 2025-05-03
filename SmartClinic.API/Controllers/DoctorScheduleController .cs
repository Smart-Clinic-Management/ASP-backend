using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule; // إضافة هذا الـ namespace
using SmartClinic.API.Bases;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule.SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;

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

        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeleteScheduleById(int scheduleId)
        {
            var response = await _doctorScheduleService.DeleteScheduleByIdAsync(scheduleId);

            if (!response.IsSuccessful)
            {
                return NotFound(new { message = response.Message });
            }

            return NewResult(new SmartClinic.Application.Bases.Response<string>
            {
                Message = response.Message
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleRequest request)
        {
            var response = await _doctorScheduleService.CreateAsync(request);

            return NewResult(new SmartClinic.Application.Bases.Response<CreateDoctorScheduleResponse>
            {
                Data = response,
                Message = "Schedule created successfully"
            });
        }
  
        [HttpPut("{scheduleId}")]
        public async Task<IActionResult> UpdateDoctorSchedule(int scheduleId, [FromBody] UpdateDoctorScheduleRequest request)
        {
            var response = await _doctorScheduleService.UpdateAsync(request);

            return NewResult(new SmartClinic.Application.Bases.Response<UpdateDoctorScheduleResponse>
            {
                Data = response,
                Message = response.Message
            });
        }
    }
}
