using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Net.Mail;
using System.Net;

namespace PhoneBook
{
    public class EmailFunction
    {
        public void Send(){
            Console.Clear();
            AnsiConsole.MarkupLine("[bold purple] Enter the receiver mail [/]");
            MailAddress to = new MailAddress(Console.ReadLine());

            AnsiConsole.MarkupLine("[bold purple] Enter the sender mail [/]");
            MailAddress from = new MailAddress(Console.ReadLine());

            MailMessage mail = new MailMessage(from, to);

            AnsiConsole.MarkupLine("[bold purple] Subject [/]");
            mail.Subject = Console.ReadLine();

            AnsiConsole.MarkupLine("[bold purple] Message [/]");
            mail.Body = Console.ReadLine();

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password");

            smtp.EnableSsl = true;
            AnsiConsole.WriteLine("Sending email...");
            try{
                smtp.Send(mail);
            }
            catch(Exception e){
                AnsiConsole.MarkupLine($"[bold red] {e.Message} [/]");
                string error = @"The SMTP server requires a secure connection or the client was not 
authenticated. The server response was: 5.7.0 Authentication Required. For more 
information, go to ";
                if(e.Message == error)
                    AnsiConsole.MarkupLine("[red] Gmail no longer supports less secure apps, \n and you need to use an App Password if you have 2-step verification enabled \n for your Google account.");
            };

        }

        
    }
}