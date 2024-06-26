using System.Net.Sockets;
using MailKit.Net.Smtp;
using MimeKit;

public class EmailService : IMessagingService
{
    public void Send(Contact to, string title, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Jonathan M", "Test@email.com"));
        message.To.Add(new MailboxAddress(to.Name, to.EmailAddresses.First().EmailAddress));
        message.Subject = title;

        message.Body = new TextPart("plain")
        { Text = body };

        using (var client = new SmtpClient())
        {
            try
            {
                client.Connect("127.0.0.1", 25, false);

                // Note: only needed if the SMTP server requires authentication
                // client.Authenticate("username", "password");

                client.Send(message);
                client.Disconnect(true);
            }
            catch (SocketException)
            {
                Console.Beep();
                Console.WriteLine("Could not connect to a SmtpClient. Please make sure you configure the SmtpClient.");
                Console.WriteLine("Or use a dummy service such as Papercut.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
            }

        }
    }
}
