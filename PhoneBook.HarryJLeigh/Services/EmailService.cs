using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Phonebook.Controllers;
using Phonebook.Models;
using Phonebook.Utilities;
using Spectre.Console;

namespace Phonebook.Services;

public class EmailService
{
    private readonly PhonebookController _phonebookController = new PhonebookController();
    
    internal void SendEmail()
    {
        Email email = EmailExtensions.CreateEmail(_phonebookController);
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