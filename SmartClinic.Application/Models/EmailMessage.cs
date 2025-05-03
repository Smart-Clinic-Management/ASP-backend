namespace SmartClinic.Application.Models;
public class EmailMessage
{
    public required string To { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}