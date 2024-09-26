using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Phonebook.Library.Helpers;

namespace Phonebook.Library;

public class MailService : Singleton<MailService>
{
    public void SendMailMessage(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("test.mailkit.jakub@gmail.com", "cffcwflzgrsfvkea");            
            client.Send(mailMessage);
            client.Disconnect(true);
        }
    }
}
