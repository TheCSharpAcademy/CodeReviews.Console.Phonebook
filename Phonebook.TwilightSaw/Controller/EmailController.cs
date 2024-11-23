using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using MimeKit;
using TwilightSaw.Phonebook.Model;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TwilightSaw.Phonebook.Controller
{
    public class EmailController
    {
        public void SendEmail(Contact contact, string emailText)
        {
           var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Oleksii","twighly1@gmail.com"));
            message.To.Add(new MailboxAddress("Oleksii", contact.Email));
            message.Subject = "Test";

            message.Body = new TextPart("plain")
            {
                Text = emailText
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate("twighly1@gmail.com", "tlxk ywrx qzwb ipej");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
