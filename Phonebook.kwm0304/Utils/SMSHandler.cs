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
    _accountSid = Environment.GetEnvironmentVariable("TWILIO_SID") ?? string.Empty;
    _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN") ?? string.Empty;
    _twilioNumber = Environment.GetEnvironmentVariable("TWILIO_NUMBER") ?? string.Empty;
  }

  public async Task<string> SendSms(Contact contact)
  {
    if (string.IsNullOrEmpty(_accountSid) || string.IsNullOrEmpty(_authToken) || string.IsNullOrEmpty(_twilioNumber))
    {
      AnsiConsole.WriteLine("Cannot send SMS: Twilio credentials are missing.");
      return string.Empty;
    }
    try 
    {
    string numString = contact.ContactPhoneInt.ToString();
    AnsiConsole.WriteLine("TO:   " + numString);
    AnsiConsole.WriteLine("TOKEN: " + _authToken + "*************");
    string messageBody = AnsiConsole.Ask<string>($"Enter your message to {contact.ContactName}:\n");
    var message = await MessageResource.CreateAsync(
      body: messageBody,
      from: new Twilio.Types.PhoneNumber(_twilioNumber),
      to: new Twilio.Types.PhoneNumber("+1" + numString)
    );
    return message.Sid;
    }
    catch (Exception e)
    {
      AnsiConsole.WriteLine($"Error: {e.Message}");
      return string.Empty;
    }
  }
}