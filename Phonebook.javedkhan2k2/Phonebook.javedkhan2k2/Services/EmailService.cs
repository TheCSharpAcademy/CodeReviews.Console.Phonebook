
using System.Net;
using System.Net.Mail;
using Spectre.Console;

namespace Phonebook.Services;

internal class EmailService
{
    private string _senderEmail;
    private string _senderPassword;
    private string _smtpHost;
    private int _smtpPort;

    public EmailService(string senderEmail, string senderPassword, string smtpHost, int smtpPort)
    {
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
        _smtpHost = smtpHost;
        _smtpPort = smtpPort;
    }

    internal void SendEmail(string subject, string body, string recipientEmail)
    {
        MailMessage mail = new MailMessage(_senderEmail, recipientEmail)
        {
            Subject = subject,
            Body = body,
        };

        SmtpClient smtpClient = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true,
        };

        try
        {
            smtpClient.Send(mail);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            AnsiConsole.Write($"Failed to send email: {ex.Message}\n");
        }

        VisualizationEngine.DisplayContinueMessage();
    }

}