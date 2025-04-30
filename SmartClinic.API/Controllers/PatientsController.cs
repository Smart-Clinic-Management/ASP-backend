using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
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
            return Ok(response);
        }
    }
}
