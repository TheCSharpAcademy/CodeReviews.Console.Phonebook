using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Phonebook.Services;

internal class SmsService
{
    private string _accountSid;
    private string _authToken;
    private string _fromPhoneNumber;

    public SmsService(string accountSid, string authToken, string fromPhoneNumber)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _fromPhoneNumber = fromPhoneNumber;
    }

    public void SendSms(string messageBody, string receiverPhoneNumber)
    {
        TwilioClient.Init(_accountSid, _authToken);
        try
        {
            // Send the message
            var message = MessageResource.Create(
                body: messageBody,
                from: new Twilio.Types.PhoneNumber(_fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber(receiverPhoneNumber)
            );
            Console.WriteLine($"Message sent successfully with SID: {message.Sid}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        VisualizationEngine.DisplayContinueMessage();
    }

}