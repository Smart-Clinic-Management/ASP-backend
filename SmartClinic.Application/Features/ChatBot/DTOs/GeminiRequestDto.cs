using System.Text.Json.Serialization;

namespace SmartClinic.Application.DTOs.ChatBot
{
    public class GeminiRequestDto
    {
        [JsonPropertyName("contents")]
        public List<GeminiContent> Contents { get; set; }
    }

    public class GeminiContent
    {
        [JsonPropertyName("parts")]
        public List<GeminiPart> Parts { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }

    public class GeminiPart
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
