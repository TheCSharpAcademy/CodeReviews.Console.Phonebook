using jollejonas.Phonebook.UserInput;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace jollejonas.Phonebook.Services
{
    public class SmsService
    {
        public static async void SendSms(string phoneNumber)
        {
            string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(
                body: ContactInput.GetBody(),
                from: new Twilio.Types.PhoneNumber("+19382015474"),
                to: new Twilio.Types.PhoneNumber("+45" + phoneNumber));

            Console.WriteLine(message.Body);
        }

    }
}
