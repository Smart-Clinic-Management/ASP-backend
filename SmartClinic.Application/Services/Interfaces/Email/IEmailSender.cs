namespace SmartClinic.Application.Services.Interfaces.Email;
public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage email);
}
