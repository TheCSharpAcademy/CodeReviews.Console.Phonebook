using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.AspNet.Core;
using Spectre.Console;
using Phonebook.kwm0304.Models;

namespace Phonebook.kwm0304.Services;

public class SMSService
{
  private readonly string _accountSid;
  private readonly string _authToken;
  private readonly string _twilioNumber;
  public SMSService()
  {
    _accountSid = Environment.GetEnvironmentVariable("TWILIO_SID")!;
    _authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")!;
    _twilioNumber = Environment.GetEnvironmentVariable("TWILIO_NUMBER")!;
  }

  public string SendSms(Contact contact)
  {
    string numString = contact.ContactPhoneInt.ToString();
    string messageBody = AnsiConsole.Ask<string>("Enter your message:\n");
    var message = MessageResource.Create(
      body: messageBody,
      from: new Twilio.Types.PhoneNumber(_twilioNumber),
      to: new Twilio.Types.PhoneNumber(numString)
    );
    return message.Sid;
  }

  public TwiMLResult ReceiveSms(string from, string messageBody)
  {
    var response = new MessagingResponse();
    AnsiConsole.MarkupLine($"[bold blue]Received message from: {from}: \n{messageBody}");
    string res = AnsiConsole.Ask<string>("How would you like to respond?");

    response.Message($"You responded: {res}");
    return new TwiMLResult(response);
  }
}
