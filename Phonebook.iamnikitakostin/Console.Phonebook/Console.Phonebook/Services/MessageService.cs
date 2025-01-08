using System.Net.Mail;
using Console.Phonebook.Controllers;
using Console.Phonebook.Validations;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Console.Phonebook.Services;
internal class MessageService : ConsoleController
{
    private readonly IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("authentication.json", optional: false, reloadOnChange: true)
    .Build();

    public bool CheckIfEmailConfigured()
    {
        string isEmailActive = config["Accounts:Email:IsActive"];
        string email = config["Accounts:Email:Address"];

        if (!Validation.Email(email))
        {
            ErrorMessage("Dear user, make sure you have entered proper information for your Email in the authentication.json file.");
            return false;
        }

        if (isEmailActive == "0" || email == null || email == "email@example.com")
        {
            ErrorMessage("Dear user, beware that your Email account was not configured.\nYou won't be able to send messages to your contacts.\nTo configure it, close the app, open 'authentication.json' and change the configuration of the Email.");
            return false;
        }

        return true;
    }

    public bool CheckIfPhoneConfigured()
    {
        string isPhoneActive = config["Accounts:Phone:IsActive"];
        string accountId = config["Accounts:Phone:TwilioAccountSid"];
        string phoneNumber = config["Accounts:Phone:TwilioPhoneNumber"];

        if (!Validation.PhoneNumber(phoneNumber))
        {
            ErrorMessage("Dear user, make sure you have entered proper information for your Phone Number in the authentication.json file.");
            return false;
        }

        if (isPhoneActive == "0" || accountId == null || accountId == "1234" || phoneNumber == null || phoneNumber == "+19999999999")
        {
            ErrorMessage("Dear user, beware that your Phone account was not configured properly.\nYou won't be able to send messages to your contacts.\nTo configure it, close the app, open 'authentication.json' and change the configuration of the Phone.");
            return false;
        }

        return true;
    }

    public bool SendEmail(string receiver, string subject, string text)
    {
        string sender = config["Accounts:Email:Address"];

        try
        {
            MailMessage mailMessage = new MailMessage(sender, receiver);
            mailMessage.From = new MailAddress(sender);
            mailMessage.To.Add(receiver);
            mailMessage.Subject = subject;
            mailMessage.Body = text;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "google.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("email", "password");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);

            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"There has been an error while sending the message {ex.Message}");
            return false;
        }
    }

    public bool SendSms(string receiver, string text)
    {
        string accountId = config["Accounts:Phone:TwilioAccountSid"];
        string token = config["Accounts:Phone:TwilioAuthToken"];
        string sender = config["Accounts:Phone:TwilioPhoneNumber"];

        try
        {
            TwilioClient.Init(accountId, token);

            MessageResource.Create(
                to: new PhoneNumber(receiver),
                from: new PhoneNumber(sender),
                body: text
            );

            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage($"There has been an error while sending the message {ex.Message}");
            return false;
        }
    }
}