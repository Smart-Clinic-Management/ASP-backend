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


    [Authorize(Roles = "admin")]
    [HttpGet]
    [ProducesResponseType<Response<Pagination<GetAllPatientsResponse>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPatients([FromQuery] GetAllPatientsParams allPatientsParams)
    {
        return NewResult(await _patientService.GetAllPatientsAsync(allPatientsParams));
    }



    //[Authorize(Roles = "admin")]
    //[HttpGet("GetAll")]
    //[ProducesResponseType<Response<List<GetAllPatientsResponse>>>(StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetAllPatients(int pageSize = 20, int pageIndex = 1)
    //{
    //    var response = await _patientService.GetAllPatientsAsync(pageSize, pageIndex);
    //    return NewResult(response);
    //}


    //[Authorize(Roles = "admin,doctor")]
    //[HttpGet("GetById/{patientId}")]
    //[ProducesResponseType<Response<GetPatientByIdResponse>>(StatusCodes.Status200OK)]
    //[ProducesResponseType<Response<GetPatientByIdResponse>>(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetPatientById(int patientId)
    //{
    //    var response = await _patientService.GetPatientByIdAsync(patientId);
    //    return NewResult(response);
    //}

}
