using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SmartClinic.Application.Models;
using SmartClinic.Application.Services.Interfaces.Email;

namespace SmartClinic.Application.Services.Implementation.EmailService;
public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    public EmailSettings EmailSettings => emailSettings.Value;

    public async Task<bool> SendEmailAsync(EmailMessage email)
    {
        var client = new SendGridClient(EmailSettings.ApiKey);

        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = EmailSettings.FromAddress,
            Name = EmailSettings.FromName
        };

        var message = MailHelper
            .CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
