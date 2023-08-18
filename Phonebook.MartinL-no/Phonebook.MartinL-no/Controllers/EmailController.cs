using System.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

using Phonebook.MartinL_no.Models;

namespace Phonebook.MartinL_no.Controllers;

internal class EmailController
{
    public static async Task SendEmail(Contact contact, string subject, string content)
    {
        var apiKey = ConfigurationManager.AppSettings.Get("twilio");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("example@email.com", "Example User");
        var to = new EmailAddress(contact.Email, contact.Name);
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}