using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;

namespace Phonebook.samggannon.Services;

public class EmailSender
{
    /// <summary>
    /// This program serves as a demonstration of fundamental skills in programmatically sending emails utilizing SMTP protocols. 
    /// As a means of achieving this functionality without disclosing credentials, FluentEmail library was utilized.
    /// </summary>


    private string _emailPDL;

    public EmailSender()
    {
        _emailPDL = PDL();
    }

    public async Task SendMail(string emailAddress, string name)
    {
        var sender = new SmtpSender(() => new SmtpClient("localhost")
        {

            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
            PickupDirectoryLocation = _emailPDL
        }) ;

        Email.DefaultSender = sender;

        try
        {
            var email = await Email
            .From("YourConsoleProject@test.com")
            .To(emailAddress, name)
            .Subject("Test Email")
            .Body("This is a test email send from a console project. Complimentary of the C# Academy")
            .SendAsync();
        }
        catch(Exception ex)
        {
            string msg = ex.Message;
        }
    }

    private static string PDL()
    {
        string userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
        string logDirectory = Path.Combine(userProfile, "Development", "Console.Phonebook.TestEmails");

        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        return logDirectory;
    }
}
