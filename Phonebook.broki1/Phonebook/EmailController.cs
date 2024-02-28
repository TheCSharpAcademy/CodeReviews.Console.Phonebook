using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Phonebook;

internal class EmailController
{

    internal static void SendEmail(MailMessage email)
    {
        var fromEmail = ConfigurationManager.AppSettings.Get("email");
        var fromEmailPassword = ConfigurationManager.AppSettings.Get("emailPassword");

        // sets up the Smtp Client
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        // tells Smtp Client that specific credentials will be used
        smtp.UseDefaultCredentials = false;

        // required, email needs to be secure/encrypted
        smtp.EnableSsl = true;
        // fromEmail credentials set
        smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);

        try
        {
            smtp.Send(email);
            Console.WriteLine("Email sent successfully!");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
            Console.ReadLine();
        }
    }

}
