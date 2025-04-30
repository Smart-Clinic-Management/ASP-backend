using Microsoft.AspNetCore.Mvc;
using SmartClinic.API.Bases;
using SmartClinic.Infrastructure.Interfaces;

namespace SmartClinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AppointmentsController(IUnitOfWork unitOfWork) : AppControllerBase
{
    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientAppointments(int patientId)
    {
        var result = await unitOfWork.Repository<IAppointment>().ListPatientAppointmentsAsync(patientId);
        return Ok(result);
    }
}
