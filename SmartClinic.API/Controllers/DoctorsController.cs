namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController(IDoctorService doctorService) : AppControllerBase
{

    [HttpGet("{id}")]
    [ProducesResponseType<Response<GetDoctorByIdResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetDoctorByIdResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await doctorService.GetDoctorByIdAsync(id));
    }


    [Authorize(Roles = "doctor")]
    [HttpPut]
    [Consumes("multipart/form-data")]
    [ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateDoctor([FromForm] UpdateDoctorRequest request)
    {
        var response = await doctorService.UpdateDoctorAsync(User.GetUserId(), request);
        return NewResult(response);
    }

    [HttpGet]
    [ProducesResponseType<Response<Pagination<GetAllDoctorsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDoctors([FromQuery] GetAllDoctorsParams allDoctorsParams)
    {
        return NewResult(await doctorService.GetAllDoctorsAsync(allDoctorsParams));
    }




    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SoftDeleteDoctor(int id)
    {
        return NewResult(await doctorService.DeleteById(id));
    }


    [Authorize(Roles = "admin")]
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType<Response<string>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<string>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
    {
        return NewResult(await doctorService.CreateDoctor(request));
    }

    [HttpGet("schedule/slots")]
    [EndpointDescription("Gets the doctor with schedules slots")]
    [ProducesResponseType<Response<GetDoctorWithSchedulesSlotsResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetDoctorWithSchedulesSlotsResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctorWithSchedulesSlots([FromQuery] GetDoctorWithSchedulesSlotsParams schedulesSlotsParams)
    {
        return NewResult(await doctorService.GetDoctorWithSchedulesSlots(schedulesSlotsParams));
    }

}
