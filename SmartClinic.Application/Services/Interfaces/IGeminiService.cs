namespace SmartClinic.Application.Services.Interfaces;

public interface IGeminiService
{

    Task<string> ProcessRequestAsync(string inputText, string language);


    Task<string> ExtractKeywordAsync(string question);

 
    string DetectLanguage(string inputText);


    Task<string> ProcessImageAsync(IFormFile imageFile, string language);


    Task<string> ProcessDocumentAsync(IFormFile documentFile, string language);


    Task<string> GetDoctorInformationAsync(string inputText);

}