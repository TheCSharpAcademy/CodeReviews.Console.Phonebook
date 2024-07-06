using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Phonebook.ukpagrace.Mail
{
    class Email
    {

        public void SendEmail(string receiver)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string? password = config.GetSection("Email")["Password"];
            MailMessage message = new MailMessage(
                "noreply@gmail.com",
                receiver,
                "Phonebook",
                $"You have been added to {receiver} phonebook"
            );
            SmtpClient smtpClient = new ("smtp.gmail.com");
            NetworkCredential networkCredential = new NetworkCredential("ukpauchechi1@gmail.com", $"{password}");
            smtpClient.Credentials = networkCredential;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
