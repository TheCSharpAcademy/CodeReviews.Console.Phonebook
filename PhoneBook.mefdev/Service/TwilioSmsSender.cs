using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Spectre.Console;

namespace PhoneBook.mefdev.Service;

internal class TwilioSmsSender: INotificationSender
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _fromPhoneNumber;

    public TwilioSmsSender(string accountSid, string authToken, string fromPhoneNumber)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _fromPhoneNumber = fromPhoneNumber;
    }

    public void Send(string recipient, string message)
    {
        TwilioClient.Init(_accountSid, _authToken);

        var messageResource = MessageResource.Create(
            to: new PhoneNumber(recipient),
            from: new PhoneNumber(_fromPhoneNumber),
            body: message
        );
        Console.WriteLine($"SMS sent to {recipient}: {messageResource.Sid}");
        AnsiConsole.Confirm("Press any key to continue... ");
    }
}

