using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DoctorsController(IDoctorService doctorService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await doctorService.GetDoctorByIdAsync(id));
    }
}
