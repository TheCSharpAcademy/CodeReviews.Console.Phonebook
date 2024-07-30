using System.Net;
using System.Net.Mail;
using Phonebook.Console.Config;
using Phonebook.Console.Models;
using Phonebook.Console.UserInterface;
using Spectre.Console;

namespace Phonebook.Console.Services;

public class EmailService {
    private AppConfig config;
    public EmailService(AppConfig appConfig)
    {
       config = appConfig; 
    }
    public void SendEmail(Contact recipient) {

        string message = UI.GetResponse("Enter the message for the recipient:");
        string from = UI.GetEmail("Enter [green]your / the sender's[/] email address:");
        string to = recipient.Email!;
        string subject = "Hi from C#";
        string body = $@"
            <html>
                <body>
                    <h1>Hi there {recipient.Name}!</h1>
                    <p>{message}</p>
                </body>
            </html> 
        ";

        try
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(config.GetEmailUsername(), config.GetEmailPassword()),
                EnableSsl = true,
                Timeout = 20000
            };

            MailMessage mailMessage = new MailMessage(from, to, subject, body){
                IsBodyHtml = true,
            };

            smtpClient.Send(mailMessage);
        }

        catch (Exception)
        {
            throw;
        } 
    }
}