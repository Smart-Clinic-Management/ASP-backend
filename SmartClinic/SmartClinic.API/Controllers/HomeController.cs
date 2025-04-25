using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "doctor")]

        public async Task<IActionResult> Get()
        {
            return Ok();
        }

    }
}
