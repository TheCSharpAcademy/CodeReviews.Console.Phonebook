using NUnit.Framework.Internal.Execution;
using Spectre.Console;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PhoneBook.Doc415;

internal class SMShandler
{
    static string accountSid;
    static string authToken;
    static string twilioPhoneNumber;
    static bool isSmsFunctional = false;

    public static void Setup()
    {
        if (!File.Exists("SMSconfig.bin"))
        {
            if (!AnsiConsole.Confirm("Would you like to setup SMS sender?"))
            {
                isSmsFunctional = false;
                return;
            }
            Console.WriteLine("You need a Twilio account to use SMS functionality. You can get a free trial account.");
            accountSid = AnsiConsole.Ask<string>("Enter Twilio Sid: ");
            authToken = AnsiConsole.Ask<string>("Enter auth Token: ");
            twilioPhoneNumber = AnsiConsole.Ask<string>("Enter your twilio phone number (+18773332244): ");
            isSmsFunctional = true;

            using Stream stream = File.Open("SMSconfig.bin", FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(stream);
            writer.Write(accountSid);
            writer.Write(authToken);
            writer.Write(twilioPhoneNumber);
            Console.WriteLine("Setup Completed. Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            using Stream stream = File.Open("SMSconfig.bin", FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(stream);
            accountSid = reader.ReadString();
            authToken = reader.ReadString();
            twilioPhoneNumber = reader.ReadString();
            isSmsFunctional = true;
        }
    }

    public static void SendSMS(string _phoneNumber, string _SMSmessage)
    {
        try
        {
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: _SMSmessage,
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(_phoneNumber)
            );
            Console.WriteLine("SMS sent. Press Enter to continue...");
            Console.ReadLine();
            Console.Clear() ;
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was a problem sending SMS: "+ex.Message);
        }
    }

    public static bool isFunctional()
    {
        return isSmsFunctional;
    }
}
