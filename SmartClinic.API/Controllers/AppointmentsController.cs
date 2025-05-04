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


    [Authorize(Roles = "admin")]
    [HttpGet("GetAll")]
    [ProducesResponseType<Response<List<AppointmentResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<AppointmentResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAppointments(int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService.ListAllAppointmentsAsync(pageSize, pageIndex);
        return NewResult(result);
    }

    [Authorize(Roles = "doctor")]
    [HttpGet("GetDoctorAppointments")]
    [ProducesResponseType<Response<List<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctorAppointments(int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService
            .ListDoctorAppointmentsAsync(User.GetUserId(), pageSize, pageIndex);
        return NewResult(result);
    }


    [Authorize(Roles = "patient")]
    [HttpGet("GetPatientAppointments")]
    [ProducesResponseType<Response<List<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<List<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientAppointments(int pageSize = 20, int pageIndex = 1)
    {
        var result = await _appointmentService
            .ListPatientAppointmentsAsync(User.GetUserId(), pageSize, pageIndex);
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


    [HttpPut("{id}")]
    [Authorize(Roles = "doctor")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    public IActionResult UpdateAppointment(int id)
    {
        return NewResult(new ResponseHandler().Success(""));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "patient")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    public IActionResult DeleteAppointment(int id)
    {
        return NewResult(new ResponseHandler().Deleted<string>());
    }


}
