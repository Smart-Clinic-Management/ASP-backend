using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Appointments.Query.DTOs.AllAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.DoctorAppointments;
using SmartClinic.Application.Features.Appointments.Query.DTOs.PatientAppointments;

namespace SmartClinic.API.Controllers;

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
    [ProducesResponseType<Response<List<AppointmentResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<AppointmentResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAppointments(int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService.ListAllAppointmentsAsync(pageSize, pageIndex);
        return NewResult(result);
    }

    [HttpGet("GetDoctorAppointments/{doctorId}")]
    [ProducesResponseType<Response<List<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctorAppointments(int doctorId, int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService.ListDoctorAppointmentsAsync(doctorId, pageSize, pageIndex);
        return NewResult(result);
    }

    [HttpGet("GetPatientAppointments/{patientId}")]
    [ProducesResponseType<Response<List<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientAppointments(int patientId, int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService.ListPatientAppointmentsAsync(patientId, pageSize, pageIndex);
        return NewResult(result);
    }

    [Authorize(Roles = "patient")]
    [HttpPost]
    [ProducesResponseType<Response<string>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAppointment(CreateAppointmentDto appointmentDto)
    {
        var patientId = User.GetUserId();
        var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
        return NewResult(result);
    }
}
