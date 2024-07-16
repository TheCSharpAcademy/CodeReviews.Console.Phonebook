using System.Net;
using System.Net.Mail;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Controllers
{
    public class EmailSMSController
    {
        private const string EmailPassword = "hjvg vbut imok ishw";
        internal static void Send(List<string> emailDetails, Contact contact)
        {
            try
            {
                var fromEmail = new MailAddress(emailDetails[1], emailDetails[0]);
                var toEmail = new MailAddress(contact.Email);
                Console.WriteLine(toEmail);

                using var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, EmailPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                using var emailMessage = new MailMessage(fromEmail,toEmail)
                {
                    Subject = emailDetails[2],
                    Body = emailDetails[3]
                };

                smtp.Send(emailMessage);

                AnsiConsole.MarkupLine("[green]Email sent successfully to: {contact.Email}[/]");
            }
            catch (Exception ex)
            {
                // Handle exception
                AnsiConsole.MarkupLine($"[bold][red]Failed to send email: {ex.Message}[/][/]");
                if (ex.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[bold][red]Inner exception: {ex.InnerException.Message}[/][/]");
                }
            }
        }
    }
}