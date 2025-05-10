using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IGeminiService
    {
        // معاملة لطلب المعالجة للنصوص والملفات مع تحديد اللغة
        Task<string> ProcessRequestAsync(string inputText, string language);

        // استخراج الكلمة المفتاحية من السؤال
        Task<string> ExtractKeywordAsync(string question);

        // تحديد اللغة بناءً على النص المدخل
        string DetectLanguage(string inputText);

        // معالجة الصورة (إذا كانت جزءًا من السؤال)
        Task<string> ProcessImageAsync(IFormFile imageFile, string language);

        // معالجة المستندات (إذا كانت جزءًا من السؤال)
        Task<string> ProcessDocumentAsync(IFormFile documentFile, string language);

        // التعامل مع الأسئلة المتعلقة بمعلومات الدكتور (مثل التخصص، الاسم، إلخ)
        Task<string> GetDoctorInformationAsync(string inputText);
    }
}
