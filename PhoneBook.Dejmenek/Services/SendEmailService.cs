using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace PhoneBook.Dejmenek.Services;

public class SendEmailService
{
    private readonly UserInteractionService _userInteractionService;
    private string? _smtpServer;
    private string? _userName;
    private string? _password;
    private string? _email;

    public SendEmailService(UserInteractionService userInteractionService)
    {
        _userInteractionService = userInteractionService;
    }

    public void Setup()
    {
        _smtpServer = "smtp.gmail.com";
        AnsiConsole.MarkupLine("Enter your Gmail SMTP server credentials: username, password and your email which mails will be sent from");
        _userName = _userInteractionService.GetUsername();
        _password = _userInteractionService.GetPassword();
        _email = _userInteractionService.GetEmail();
    }

    public void SendEmail(string recipentEmail)
    {
        SmtpClient smtpClient = new SmtpClient(_smtpServer, 587);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(_userName, _password);

        MailMessage message = new MailMessage(_email, recipentEmail);
        message.Subject = _userInteractionService.GetEmailSubject();
        message.Body = _userInteractionService.GetEmailBody();

        try
        {
            smtpClient.Send(message);
            AnsiConsole.MarkupLine("Email sent.");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"There was an error sending email: {ex.Message}");
        }
    }

}
