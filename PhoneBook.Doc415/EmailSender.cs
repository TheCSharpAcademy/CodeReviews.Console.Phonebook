using System.Net;
using System.Net.Mail;
namespace PhoneBook.Doc415;

internal class EmailSender
{
    string smtpServer = SetUpEmail.GetServer();
    string userName = SetUpEmail.GetUsername();
    string password = SetUpEmail.GetPassword();
    string subject;
    string body;
    string recipentEmail;
    string myEmail = SetUpEmail.GetUserEmail();

    public void SendEmail(string _recipientEmail, string _subject, string _body)
    {
        subject = _subject;
        body = _body;
        recipentEmail = _recipientEmail;
        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = 587,
            EnableSsl = true,
        };
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(userName, password);

        try
        {
            smtpClient.Send(myEmail, recipentEmail, subject, body);
            Console.WriteLine("Mail sent.Press enter to continue...");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error sending mail: {ex.Message}");
        }
    }
}
