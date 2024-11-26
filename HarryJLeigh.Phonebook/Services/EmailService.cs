using System.Net;
using System.Net.Mail;
using Phonebook.Models;
using Phonebook.Utilities;
using Spectre.Console;

namespace Phonebook.Services;

public static class EmailService
{
    internal static void SendEmail()
    {
        Email email = EmailExtensions.CreateEmail();
        try
        {
            SmtpClient smtpClient = new SmtpClient("your-host")
            {
                Port = 25,
                Credentials = new NetworkCredential("youremail@example.com", "your-password"),
                EnableSsl = false
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("your-address", "Phonebook"),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = false
            };

            mail.To.Add(email.Address);
            smtpClient.Send(mail);
            AnsiConsole.MarkupLine("[green]Success! Email sent.[/]");
            Util.AskUserToContinue();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            Util.AskUserToContinue();
        }
    }
}