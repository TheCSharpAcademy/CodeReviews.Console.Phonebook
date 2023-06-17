using LucianoNicolasArrieta.PhoneBook.Persistence;
using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace LucianoNicolasArrieta.PhoneBook
{
    internal class EmailSender
    {

        public void sendEmail()
        {
            UserInput userInput = new UserInput();
            ContactController contactController = new ContactController();

            string to = contactController.GetContactEmailById();
            MailAddress toAdress = new MailAddress(to);
            AnsiConsole.WriteLine($"To: {to}");
            
            AnsiConsole.Write("From ");
            MailAddress from = userInput.EmailInput();
            var fromPassword = AnsiConsole.Prompt(
                new TextPrompt<string>("Generated Google App password:")
                .PromptStyle("red")
                .Secret());

            string subject = AnsiConsole.Ask<string>("Subject: ");
            string body = AnsiConsole.Ask<string>("Body: ");

            ICredentialsByHost credentials = new NetworkCredential(from.Address, fromPassword);

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = credentials
            };

            MailMessage mail = new MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            try
            {
                smtpClient.Send(mail);
                AnsiConsole.MarkupLine("[green]Email sent!.[/] Press any key to continue");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]There was an error trying to send the email: {ex.Message}[/]. Press any key to continue");
                Console.ReadKey();
            }
        }
    }
}
