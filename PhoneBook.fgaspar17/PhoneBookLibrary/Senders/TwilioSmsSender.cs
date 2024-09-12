using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PhoneBookLibrary;

public class TwilioSmsSender : ISender
{
    public string Receiver { get; set; }

    public TwilioSmsSender(string receiver)
    {
        Receiver = receiver;
    }
    public bool Send(string body)
    {
        try
        {
            var accountSid = GlobalConfig.TwilioSid;
            var authToken = GlobalConfig.TwilioAuthToken;
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
              new PhoneNumber(Receiver));
            messageOptions.From = new PhoneNumber(GlobalConfig.TwilioPhoneNumber);
            messageOptions.Body = body;


            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending the SMS: {e.Message}");
            return false;
        }

        return true;
    }
}