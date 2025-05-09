using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Implementation;

namespace SmartClinic.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly IGeminiService _geminiService;

        public ChatBotController(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpPost("extract-keyword")]
        public async Task<IActionResult> ExtractKeyword([FromForm] QuestionRequest questionRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(questionRequest.Question))
                {
                    return BadRequest(new { Error = "The 'Question' field is required and cannot be empty." });
                }

                var keyword = await _geminiService.ExtractKeywordAsync(questionRequest.Question);

                return Ok(new { Keyword = keyword });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }

    public class QuestionRequest
    {
        public string Question { get; set; }
    }
}
