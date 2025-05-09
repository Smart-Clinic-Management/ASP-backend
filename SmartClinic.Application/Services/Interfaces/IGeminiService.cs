using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IGeminiService
    {
        // معاملة لطلب المعالجة للنصوص والملفات مع تحديد اللغة
        Task<string> ProcessRequestAsync(string inputText, IFormFile imageFile, IFormFile documentFile, string language);

        // استخراج الكلمة المفتاحية من السؤال
        Task<string> ExtractKeywordAsync(string question);

        // تحديد اللغة بناءً على النص المدخل
        string DetectLanguage(string inputText);
    }
}
