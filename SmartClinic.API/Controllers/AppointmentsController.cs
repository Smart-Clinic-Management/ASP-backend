using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.Bases;
using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : AppControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAppointments(int pageSize = 20, int pageIndex = 1)
        {
            var result = await _appointmentService.ListAllAppointmentsAsync(pageSize, pageIndex);
            return NewResult(result);
        }

        [HttpGet("GetDoctorAppointments/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId, int pageSize = 20, int pageIndex = 1)
        {
            var result = await _appointmentService.ListDoctorAppointmentsAsync(doctorId, pageSize, pageIndex);
            return NewResult(result);
        }

        [HttpGet("GetPatientAppointments/{patientId}")]
        public async Task<IActionResult> GetPatientAppointments(int patientId, int pageSize = 20, int pageIndex = 1)
        {
            var result = await _appointmentService.ListPatientAppointmentsAsync(patientId, pageSize, pageIndex);
            return NewResult(result);
        }
    }
}
