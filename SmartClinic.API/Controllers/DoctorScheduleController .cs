using SmartClinic.Application.Features.DoctorsSchedules.Query.GetDoctorSchedule;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorScheduleController(IDoctorScheduleService doctorScheduleService) : AppControllerBase
{
    private readonly IDoctorScheduleService _doctorScheduleService = doctorScheduleService;


    [Authorize(Roles = "admin")]
    [HttpDelete]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteScheduleById([FromBody] DeleteDoctorScheduleRequest deleteDoctorSchedule)
    {
        var response = await _doctorScheduleService.DeleteScheduleAsync(deleteDoctorSchedule);

        return NewResult(response);
    }


    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleRequest request)
    {
        var result = await _doctorScheduleService.CreateAsync(request);

        return NewResult(result);

    }

}
