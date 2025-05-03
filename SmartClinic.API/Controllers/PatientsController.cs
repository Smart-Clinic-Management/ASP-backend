using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.DoctorSchedule.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients;

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
    [ProducesResponseType<Response<List<GetAllPatientsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPatients(int pageSize = 20, int pageIndex = 1)
    {
        var response = await _patientService.GetAllPatientsAsync(pageSize, pageIndex);
        return NewResult(response);
    }

    [HttpGet("GetById/{patientId}")]
    [ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<GetDoctorSchedule>>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatientById(int patientId)
    {
        var response = await _patientService.GetPatientByIdAsync(patientId);
        return NewResult(response);
    }

}
