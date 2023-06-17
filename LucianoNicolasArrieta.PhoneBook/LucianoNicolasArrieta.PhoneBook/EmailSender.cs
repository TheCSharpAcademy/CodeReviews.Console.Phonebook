using Spectre.Console;
using System.Net;
using System.Net.Mail;

namespace LucianoNicolasArrieta.PhoneBook
{
    internal class EmailSender
    {

        public void sendEmail()
        {
            MailAddress from = MailInput("From");
            var fromPassword = AnsiConsole.Prompt(
                new TextPrompt<string>("Generated Google App password:")
                .PromptStyle("red")
                .Secret());

            MailAddress to = MailInput("To");
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


        private MailAddress MailInput(string aux)
        {
            MailAddress mail = null;

            while (mail == null)
            {
                string from = AnsiConsole.Ask<string>($"{aux}: ");
                try
                {
                    mail = new MailAddress(from);
                }
                catch
                {
                    AnsiConsole.MarkupLine("[red]Invalid email. Try again.[/]");
                }
            }

            return mail;
        }
    }
}
