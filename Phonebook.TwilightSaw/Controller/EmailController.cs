using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TwilightSaw.Phonebook.Model;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TwilightSaw.Phonebook.Controller
{
    public class EmailController()
    {

        public void SendEmail(Contact contact, string emailText, string head)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(contact.Name, configuration["CustomSettings:YourEmail"]));
            message.To.Add(new MailboxAddress("Me", contact.Email));
            message.Subject = head;

            message.Body = new TextPart("plain")
            {
                Text = emailText
            };

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            client.Authenticate(configuration["CustomSettings:YourEmail"], configuration["CustomSettings:YourAppPassword"]);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
