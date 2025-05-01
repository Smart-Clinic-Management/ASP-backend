using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.Bases;
using SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor;
using SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctorWithAvailableAppointment;
using SmartClinic.Application.Services.Interfaces;

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
    public async Task<IActionResult> GetById(int id)
    {
        return NewResult(await _doctorService.GetDoctorByIdAsync(id));
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromForm] UpdateDoctorRequest request)
    {
        var response = await _doctorService.UpdateDoctorAsync(id, request);
        return NewResult(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllDoctors(int pageSize = 20, int pageIndex = 1)
    {
        return NewResult(await _doctorService.GetAllDoctorsAsync(pageSize, pageIndex));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteDoctor(int id)
    {
        return NewResult(await _doctorService.SoftDeleteDoctorAsync(id));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
    {
        return NewResult(await _doctorService.CreateDoctor(request));
    }

    [HttpGet("schedule")]
    [EndpointDescription("Gets the doctor with available schedule")]
    [ProducesResponseType<GetDoctorWithAvailableAppointment>(200)]
    public async Task<IActionResult> GetDoctorAvailableSchedules([FromQuery] int id, [FromQuery] DateOnly startDate)
    {
        return NewResult(await _doctorService.GetDoctorWithAvailableSchedule(id, startDate));
    }

}
