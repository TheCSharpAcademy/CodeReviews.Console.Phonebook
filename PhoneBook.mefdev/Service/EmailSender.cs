
using SendGrid;
using SendGrid.Helpers.Mail;
using Spectre.Console;

namespace PhoneBook.mefdev.Service;

public class EmailSender : INotificationSender
{
    private readonly string _sendGridApiKey;
    private readonly string _company;
    private readonly string _fromEmail;

    public EmailSender(string sendGridApiKey, string company, string fromEmail)
    {
        _sendGridApiKey = sendGridApiKey;
        _company = company;
        _fromEmail = fromEmail;
    }

    public async void Send(string recipient, string message)
    {
        var client = new SendGridClient(_sendGridApiKey);
        var from = new EmailAddress(_fromEmail, _company);
        var subject = "Welcome to Our Service!";
        var to = new EmailAddress(recipient);
        var plainTextContent = message;
        var htmlContent = $"<strong>{message}</strong>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        try
        {
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Email sent to {recipient}. Response: {response.StatusCode}");
            AnsiConsole.Confirm("Press any key to continue... ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}