using Twilio.Rest.Api.V2010.Account;
using Spectre.Console;
using Phonebook.kwm0304.Models;
using Twilio;

namespace Phonebook.kwm0304.Utils;

public class SmsHandler
{
  private readonly string _accountSid;
  private readonly string _authToken;
  private readonly string _twilioNumber;
  public SmsHandler()
  {
    _accountSid = Environment.GetEnvironmentVariable("TWILIO_SID")
        ?? throw new InvalidOperationException("TWILIO_SID environment variable is not set.");
    _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
        ?? throw new InvalidOperationException("TWILIO_AUTH_TOKEN environment variable is not set.");
    _twilioNumber = Environment.GetEnvironmentVariable("TWILIO_NUMBER")
        ?? throw new InvalidOperationException("TWILIO_NUMBER environment variable is not set.");

    TwilioClient.Init(_accountSid, _authToken);
  }

  public async Task<string> SendSms(Contact contact)
  {
    string numString = contact.ContactPhoneInt.ToString();
    AnsiConsole.WriteLine("TO:   " + numString);
    AnsiConsole.WriteLine("TOKEN: " +_authToken + "*************");
    string messageBody = AnsiConsole.Ask<string>($"Enter your message to {contact.ContactName}:\n");
    var message = await MessageResource.CreateAsync(
      body: messageBody,
      from: new Twilio.Types.PhoneNumber(_twilioNumber),
      to: new Twilio.Types.PhoneNumber("+1" + numString)
    );
    return message.Sid;
  }
}