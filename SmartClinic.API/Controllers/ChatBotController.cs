using Microsoft.AspNetCore.Mvc;
using SmartClinic.Application.DTOs.ChatBot;
using SmartClinic.Application.Interfaces;
using System.Threading.Tasks;

namespace SmartClinic.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] ChatBotRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Question))
            {
                return BadRequest("السؤال لا يمكن أن يكون فارغًا.");
            }

            var response = await _chatBotService.GetChatBotResponseAsync(request);
            return Ok(response);
        }
    }
}
