using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.Services.Implementation;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SmartClinic.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly IGeminiService _geminiService;
        private readonly ILogger<ChatBotController> _logger;

        public ChatBotController(IGeminiService geminiService, ILogger<ChatBotController> logger)
        {
            _geminiService = geminiService;
            _logger = logger;
        }

        /// <summary>
        /// استخراج الكلمة المفتاحية أو الرد على استفسارات المواعيد أو الأسئلة الطبية
        /// </summary>
        [HttpPost("extract-keyword")]
        public async Task<IActionResult> ExtractKeyword([FromForm] QuestionRequest questionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var keyword = await _geminiService.ExtractKeywordAsync(questionRequest.Question);
                return Ok(new { Keyword = keyword });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while extracting the keyword.");
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }

    public class QuestionRequest
    {
        [Required(ErrorMessage = "The 'Question' field is required and cannot be empty.")]
        public string Question { get; set; }
    }
}
