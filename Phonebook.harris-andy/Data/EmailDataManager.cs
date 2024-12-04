using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace Phonebook.Data;

public class EmailDataManager
{
    internal static readonly IConfiguration _config;

    static EmailDataManager()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }

    internal static void SendEmail(MimeMessage message)
    {
        using (var client = new SmtpClient())
        {
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(username, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}