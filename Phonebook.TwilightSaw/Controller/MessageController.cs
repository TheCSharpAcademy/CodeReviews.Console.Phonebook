using System.Text;
using Microsoft.Extensions.Configuration;
using TwilightSaw.Phonebook.Helpers;
using TwilightSaw.Phonebook.Model;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilightSaw.Phonebook.Controller;

public class MessageController
{
    public void SendMessage(Contact contact, string messageText)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        //Twilio
        var accountSid = configuration["CustomSettings:YourSID"]; 
        var authToken = configuration["CustomSettings:YourAuthToken"];

        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
            new PhoneNumber(contact.PhoneNumber)); //this phone number must be registered in Twilio to get messages
        messageOptions.From = new PhoneNumber(configuration["CustomSettings:YourPhoneNumber"]);
        messageOptions.Body = messageText;
        var message = MessageResource.Create(messageOptions);

        Validation.EndMessage(message.Body);
    }
}

