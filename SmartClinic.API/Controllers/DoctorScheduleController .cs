using SmartClinic.Application.Features.DoctorsSchedules.Command.DeleteDoctorSchedule;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorScheduleController(IDoctorScheduleService doctorScheduleService) : AppControllerBase
{
    private readonly IDoctorScheduleService _doctorScheduleService = doctorScheduleService;

    //[HttpGet("GetByDoctor/{doctorId}")]
    //[ProducesResponseType<Response<IEnumerable<GetDoctorSchedule>>>(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetScheduleForDoctor(int doctorId)
    //{
    //    var response = await _doctorScheduleService.GetSchedulesForDoctorAsync(doctorId);

    //    if (response == null || !response.Data.Any())
    //    {
    //        return NotFound(new { message = $"No schedules found for doctor {doctorId}" });
    //    }

    //    return NewResult(response);
    //}


    [Authorize(Roles = "admin")]
    [HttpDelete]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteScheduleById([FromQuery] DeleteDoctorScheduleRequest deleteDoctorSchedule)
    {
        var response = await _doctorScheduleService.DeleteScheduleAsync(deleteDoctorSchedule);

        return NewResult(response);
    }


    //[Authorize(Roles = "doctor")]
    //[HttpPost]
    //[ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status201Created)]
    //public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleRequest request)
    //{
    //    var response = await _doctorScheduleService.CreateAsync(request);

    //    return NewResult(response);

    //}


    //[Authorize(Roles = "doctor")]
    //[HttpPut("{scheduleId}")]
    //[ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> UpdateDoctorSchedule(int scheduleId, [FromBody] UpdateDoctorScheduleRequest request)
    //{
    //    var response = await _doctorScheduleService.UpdateAsync(request);

    //    return NewResult(response);
    //}
}
