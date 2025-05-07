using SmartClinic.Application.DTOs.ChatBot;
using System.Threading.Tasks;

namespace SmartClinic.Application.Interfaces
{
    public interface IChatBotService
    {
        Task<ChatBotResponseDto> GetChatBotResponseAsync(ChatBotRequestDto requestDto);
    }
}
