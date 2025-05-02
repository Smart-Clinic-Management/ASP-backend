using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.Bases;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : AppControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllPatients(int pageSize = 20, int pageIndex = 1)
    {
        var response = await _patientService.GetAllPatientsAsync(pageSize, pageIndex);
        return NewResult(response);
    }

    [HttpGet("GetById/{patientId}")]
    public async Task<IActionResult> GetPatientById(int patientId)
    {
        var response = await _patientService.GetPatientByIdAsync(patientId);
        return NewResult(response);
    }

}
