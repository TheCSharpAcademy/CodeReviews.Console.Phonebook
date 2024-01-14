using PhoneBook.Doc415.Models;
using Spectre.Console;
using System.Net;
using System.Net.Mail;
namespace PhoneBook.Doc415;

internal class EmailSender
{
    string smtpServer;
    string userName;
    string password;
    string subject;
    string body;
    string recipentEmail;
    string myEmail;
    public void SetUpSender()
    {
        smtpServer = AnsiConsole.Ask<string>("Enter your smtp server (smtp.gmail.com) (alpha stage no validation yet): "); //add validation
        userName = AnsiConsole.Ask<string>("Enter user name: ");
        password = AnsiConsole.Ask<string>("Enter password: ");
        
        do
        {
            myEmail = AnsiConsole.Ask<string>("Enter your e-mail adress (foo@foo.com):  ");
            
        } while (!Validators.isValidEmail(myEmail));
        
    }

    public void SendEmail(Contact contact)
    {
        recipentEmail = contact.Email;
        Console.WriteLine($"Sending e-mail to {recipentEmail} :");
        subject = AnsiConsole.Ask<string>("Subject: ","");
        body = AnsiConsole.Ask<string>("Mail: ");

        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = 465,
            Credentials = new NetworkCredential(userName, password),
            EnableSsl = true,
        };
              

        try
        {
            smtpClient.Send(myEmail,recipentEmail,subject,body);
            Console.WriteLine("Mail sent.Press enter to continue...");
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error sending mail: {ex.Message}");
        }
    }
}
