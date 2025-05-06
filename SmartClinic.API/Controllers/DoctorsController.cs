using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;

namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : AppControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Response<GetDoctorByIdResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetDoctorByIdResponse>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetById(int id)
    //{
    //    return NewResult(await _doctorService.GetDoctorByIdAsync(id));
    //}


    //[Authorize(Roles = "doctor")]
    //[HttpPut("Update")]
    //[ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> UpdateDoctor([FromForm] UpdateDoctorRequest request)
    //{
    //    var response = await _doctorService.UpdateDoctorAsync(User.GetUserId(), request);
    //    return NewResult(response);
    //}

    [HttpGet]
    [ProducesResponseType<Response<List<GetAllDoctorsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDoctors([FromQuery] GetAllDoctorsParams allDoctorsParams)
    {
        return NewResult(await _doctorService.GetAllDoctorsAsync(allDoctorsParams));
    }


    //[Authorize(Roles = "admin")]
    //[HttpDelete("{id}")]
    //[ProducesResponseType<Response<SoftDeleteDoctorResponse>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<SoftDeleteDoctorResponse>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> SoftDeleteDoctor(int id)
    //{
    //    return NewResult(await _doctorService.SoftDeleteDoctorAsync(id));
    //}


    //[Authorize(Roles = "admin")]
    //[HttpPost("Create")]
    //[Consumes("multipart/form-data")]
    //[ProducesResponseType<Response<CreateDoctorResponse>>(StatusCodes.Status201Created)]
    //[ProducesResponseType<Response<CreateDoctorResponse>>(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
    //{
    //    return NewResult(await _doctorService.CreateDoctor(request));
    //}

    //[HttpGet("schedule")]
    //[EndpointDescription("Gets the doctor with available schedule")]
    //[ProducesResponseType<Response<GetDoctorWithAvailableAppointment>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<GetDoctorWithAvailableAppointment>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetDoctorAvailableSchedules([FromQuery] int id, [FromQuery] DateOnly startDate)
    //{
    //    return NewResult(await _doctorService.GetDoctorWithAvailableSchedule(id, startDate));
    //}

}
