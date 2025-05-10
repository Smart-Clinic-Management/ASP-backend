namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IAppointmentService appointmentService) : AppControllerBase
{
    private readonly IAppointmentService _appointmentService = appointmentService;

    [Authorize(Roles = "admin")]
    [HttpGet("admin/GetAll")]
    [ProducesResponseType<Response<Pagination<AllAppointmentsResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<Pagination<AllAppointmentsResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAppointments([FromQuery] AllAppointmentsParams appointmentsParams)
    {
        var result = await _appointmentService.ListAllAppointmentsAsync(appointmentsParams);
        return NewResult(result);
    }

    [Authorize(Roles = "doctor")]
    [HttpGet("GetDoctorAppointments")]
    [ProducesResponseType<Response<Pagination<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<Pagination<DoctorWithAppointmentsResponseDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctorAppointments([FromQuery] GetDoctorAppointmentsParams appointmentsParams)
    {
        var result = await _appointmentService
            .ListDoctorAppointmentsAsync(User.GetUserId(), appointmentsParams);
        return NewResult(result);
    }


    [Authorize(Roles = "patient")]
    [HttpGet("GetPatientAppointments")]
    [ProducesResponseType<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<Pagination<PatientAppointmentsWithDoctorDetailsDto>>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientAppointments([FromQuery] GetPatientAppointmentsParams appointmentParams)
    {
        var result = await _appointmentService
            .ListPatientAppointmentsAsync(User.GetUserId(), appointmentParams);
        return NewResult(result);
    }

    [Authorize(Roles = "patient")]
    [HttpPost]
    [ProducesResponseType<Response<string>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest appointmentDto)
    {
        var patientId = User.GetUserId();
        var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
        return NewResult(result);
    }


    [HttpPut("doctor")]
    [Authorize(Roles = "doctor")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentRequest updateDoctorAppointment)
    {
        var result = await _appointmentService
            .UpdateDoctorAppointmentAsync(User.GetUserId(), updateDoctorAppointment);

        return NewResult(result);
    }

    [HttpDelete("patient/cancel/{appointmentId}")]
    [Authorize(Roles = "patient")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> DeleteAppointment(int appointmentId)
    {
        var result = await _appointmentService
            .CancelPatientAppointmentAsync(User.GetUserId(), appointmentId);

        return NewResult(result);
    }


}
