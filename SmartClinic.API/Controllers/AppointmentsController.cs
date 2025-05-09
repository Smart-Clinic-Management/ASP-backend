using SmartClinic.Application.Features.Appointments.Command.UpdateDoctorAppointment;

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
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
    {
        var patientId = User.GetUserId();
        var result = await _appointmentService.CreateAppointmentAsync(appointmentDto, patientId);
        return NewResult(result);
    }


    [HttpPut]
    [Authorize(Roles = "doctor")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAppointment([FromBody] UpdateDoctorAppointmentRequest updateDoctorAppointment)
    {
        var result = await _appointmentService
            .UpdateDoctorAppointmentAsync(User.GetUserId(), updateDoctorAppointment);

        return NewResult(result);
    }

    //[HttpDelete("{id}")]
    //[Authorize(Roles = "patient")]
    //[ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    //public IActionResult DeleteAppointment(int id)
    //{
    //    return NewResult(new ResponseHandler().Deleted<string>());
    //}


}
