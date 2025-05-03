using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor;
using SmartClinic.Application.Features.Doctors.Command.DTOs.DeleteDoctor;
using SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctorWithAvailableAppointment;

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
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await _doctorService.GetDoctorByIdAsync(id));
    }

    [HttpPut("Update/{id}")]
    [ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Response<UpdateDoctorResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDoctor(int id, [FromForm] UpdateDoctorRequest request)
    {
        var response = await _doctorService.UpdateDoctorAsync(id, request);
        return NewResult(response);
    }

    [HttpGet("GetAll")]
    [ProducesResponseType<Response<List<GetAllDoctorsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDoctors(int pageSize = 20, int pageIndex = 1)
    {
        return NewResult(await _doctorService.GetAllDoctorsAsync(pageSize, pageIndex));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<Response<SoftDeleteDoctorResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<SoftDeleteDoctorResponse>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDeleteDoctor(int id)
    {
        return NewResult(await _doctorService.SoftDeleteDoctorAsync(id));
    }

    [HttpPost("Create")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType<Response<CreateDoctorResponse>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<CreateDoctorResponse>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
    {
        return NewResult(await _doctorService.CreateDoctor(request));
    }

    [HttpGet("schedule")]
    [EndpointDescription("Gets the doctor with available schedule")]
    [ProducesResponseType<Response<GetDoctorWithAvailableAppointment>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetDoctorWithAvailableAppointment>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDoctorAvailableSchedules([FromQuery] int id, [FromQuery] DateOnly startDate)
    {
        return NewResult(await _doctorService.GetDoctorWithAvailableSchedule(id, startDate));
    }

}
