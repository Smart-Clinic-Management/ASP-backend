using SmartClinic.Application.DTOs.ChatBot;
using SmartClinic.Application.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartClinic.Infrastructure.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _geminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";
        private readonly string _apiKey = "AIzaSyDr44a2-QwpepYzZ4bmoKq2T00qq1MQZ2E";

        public ChatBotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatBotResponseDto> GetChatBotResponseAsync(ChatBotRequestDto requestDto)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        parts = new[]
                        {
                            new { text = requestDto.Question }
                        }
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_geminiApiUrl}?key={_apiKey}", content);

            if (!response.IsSuccessStatusCode)
            {
                return new ChatBotResponseDto
                {
                    Answer = "عذرًا، حدث خطأ أثناء الاتصال بالمساعد الذكي."
                };
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);
            var answer = doc.RootElement
                            .GetProperty("candidates")[0]
                            .GetProperty("content")
                            .GetProperty("parts")[0]
                            .GetProperty("text")
                            .GetString();

            return new ChatBotResponseDto
            {
                Answer = answer
            };
        }
    }
}
