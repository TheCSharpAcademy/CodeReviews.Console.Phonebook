using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PhoneBook;
internal class MessagingService
{
    public static void SendSMS(string toPhoneNumber, string messageBody)
    {
        string? sid = Environment.GetEnvironmentVariable("TWILIO_SID");
        string? auth = Environment.GetEnvironmentVariable("TWILIO_AUTH");

        TwilioClient.Init(sid, auth);

        var message = MessageResource.Create(
            body: messageBody,
            from: new PhoneNumber("+1 646 681 6071"),
            to: new PhoneNumber(toPhoneNumber)
            );

        Console.WriteLine($"Message sent: {message.Sid}");
    }
}
