using Phonebook.Controllers;
using Phonebook.Models;
using PhoneNumbers;
using Spectre.Console;

namespace Phonebook.Services;

internal class SettingsService
{
    internal static bool CheckSettings()
    {
        bool settingsExist = false;
        Settings settings = Controllers.SettingsController.GetSettings();
        if (settings == null)
        {
            settingsExist = false;
        }
        else
        {
            settingsExist = true;
        }

        return settingsExist;
    }

    internal static Settings UpdateSettings(Settings? settings)
    {
        settings.FromMail = AnsiConsole.Confirm("Modify the from email address?")
                    ? settings.FromMail = GetFromEmailFromUser()
                    : settings.FromMail;
        settings.Password = AnsiConsole.Confirm("Modify the email server password?")
                    ? settings.Password = GetPasswordFromUser()
                    : settings.Password;
        settings.SmtpUrl = AnsiConsole.Confirm("Modify the SMTP Url?")
                    ? settings.SmtpUrl = GetSmtpUrlFromUser()
                    : settings.SmtpUrl;
        settings.Port = AnsiConsole.Confirm("Modify the SMTP server port")
                    ? settings.Port = GetPortFromUser()
                    : settings.Port;
        settings.TwilioAccountSid = AnsiConsole.Confirm("Modify the Twilio SID?")
                    ? settings.TwilioAccountSid = GetTwilioSidFromUser()
                    : settings.TwilioAccountSid;
        settings.TwilioAuthToken = AnsiConsole.Confirm("Modify the Twilio Auth Token?")
                    ? settings.TwilioAuthToken = GetTwilioAuthTokenFromUser()
                    : settings.TwilioAuthToken;
        settings.TwilioNumber = AnsiConsole.Confirm("Modify the Twilio Number?")
                    ? settings.TwilioNumber = GetTwilioNumberFromUser()
                    : settings.TwilioNumber;

        UpdateSettings(settings);

        return settings;
    }

    internal static void SettingsPrompt()
    {
        bool enterSettings = false;
        Console.WriteLine("Console PhoneBook needs you to enter some settings.  You will need...");
        Thread.Sleep(1000);
        Console.WriteLine("An email server with an email address.");
        Thread.Sleep(1000);
        Console.WriteLine("A password for the email address.");
        Thread.Sleep(1000);
        Console.WriteLine("The url and port for the email server ");
        Thread.Sleep(1000);
        Console.WriteLine("You will also need a twilio SID and tiwlio auth token as well as a twilio phone number");
        Thread.Sleep(1000);
        Console.WriteLine("If you need values for these settings you may contact Paul Saltzman.");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
        enterSettings = AnsiConsole.Confirm("Would you like to enter the setting now?")
            ? enterSettings
            : enterSettings;
        if (enterSettings)
        {
            GetSettingsFromUserFirstTime();
        }
        else
        {
            return;
        }
    }

    internal static void GetSettingsFromUserFirstTime()
    {
        int id = 1;
        Settings settings = new Settings();

        settings.SettingId = id;
        settings.FromMail = GetFromEmailFromUser();
        settings.Password = GetPasswordFromUser();
        settings.SmtpUrl = GetSmtpUrlFromUser();
        settings.Port = GetPortFromUser();
        settings.TwilioAccountSid = GetTwilioSidFromUser();
        settings.TwilioAuthToken = GetTwilioAuthTokenFromUser();
        settings.TwilioNumber = GetTwilioNumberFromUser();

        SettingsController.AddSettings(settings);
    }

    private static string GetTwilioNumberFromUser()
    {
        PhoneNumber twilioNumber;
        string formattedTwilioNumber = "";
        bool phoneValid = false;

        do
        {
            string userInput = AnsiConsole.Ask<string>("Please enter your full Twilio phone number including country code:");
            try
            {
                twilioNumber = Validators.ParsePhoneNumber(userInput);
                phoneValid = Validators.IsValidPhone(twilioNumber);
                formattedTwilioNumber = Validators.FormatNumber(twilioNumber);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                continue; // Restart the loop to prompt the user again
            }

            if (!phoneValid)
            {
                AnsiConsole.MarkupLine("[red]Invalid phone number format. Please try again.[/]");
            }

        } while (!phoneValid);

        return formattedTwilioNumber;
    }


    private static string? GetTwilioAuthTokenFromUser()
    {
        string twilioAuthToken = AnsiConsole.Ask<string>("Please enter the Twilio Auth Token.");
        return twilioAuthToken;
    }

    private static string GetTwilioSidFromUser()
    {
        string twilionSid = AnsiConsole.Ask<string>("Please enter the Twilio Sid.");
        return twilionSid;
    }

    private static int GetPortFromUser()
    {
        int port = AnsiConsole.Ask<int>("Please enter the port for the smtp service.");
        return port;
    }

    private static string? GetSmtpUrlFromUser()
    {
        string? smtpUrl;
        bool smtpValid;

        do
        {
            smtpUrl = AnsiConsole.Ask<string>("Please enter the smtp url (smpt.gmail.com)");
            smtpValid = Validators.IsValidSmtpUrl(smtpUrl);
            if (!smtpValid)
            {
                Console.WriteLine("I'm sorry that was not a valid url.");
                Console.ReadKey();

            }
        } while (!smtpValid);

        return smtpUrl;
    }

    private static string? GetPasswordFromUser()
    {
        bool pwValid = false;
        string? password0 = null;
        string? password1 = null;

        do
        {
            password0 = AnsiConsole.Prompt(
                new TextPrompt<string>("Please enter the password for the email address.")
                .Secret());

            password1 = AnsiConsole.Prompt(
                new TextPrompt<string>("Please confirm the password for the email address.")
                .Secret());

            pwValid = Validators.InvalidPassword(password0, password1);

            if (!pwValid)
            {
                Console.WriteLine("I'm sorry the passwords don't match.");
                Console.ReadKey();

            }
        } while (!pwValid);

        return password0;
    }

    private static string GetFromEmailFromUser()
    {
        string? fromEmail;
        bool emailValid = false;
        do
        {
            fromEmail = AnsiConsole.Ask<string>("Please enter the email address for the program.");
            emailValid = Validators.IsValidEmail(fromEmail);
            if (!emailValid)
            {
                Console.WriteLine("I'm sorry that was not a valid email. Please try again");
                Console.ReadKey();

            }
        } while (!emailValid);

        return fromEmail;
    }

    internal static void RemoveSettings(Settings settings)
    {
        if (settings == null)
        {
            Console.WriteLine("There are no settings to remove.");
            Console.ReadKey();
        }
        else
        {
            bool delete = false;
            delete = AnsiConsole.Confirm("Are you sure you want to remove settings? This will disable some program functionality.");

            if (delete)
            {
                SettingsController.DeleteSettings(settings);
            }
        }

    }

    internal static void ShowSettings(Settings settings)
    {
        bool settingNull = settings == null;
        var table = new Spectre.Console.Table();
        table.AddColumns("Setting", "Value");
        if (settingNull)
        {
            table.AddRow($@"Smtp EmailAddress", "not set");
            table.AddRow($@"Smtp Password", "not set");
            table.AddRow($@"Smtp Url", "not set");
            table.AddRow($@"Smtp Port", "not set");
            table.AddRow($@"Twilio Account Sid", "not set");
            table.AddRow($@"Twilio Auth Token", "not set");
            table.AddRow($@"Twilio Phone Number", "not set");

        }
        else
        {
            table.AddRow($@"Smtp EmailAddress", settings.FromMail != null ? $@"{settings.FromMail}" : "not set");
            table.AddRow($@"Smtp Password", settings.Password != null ? $@"{settings.Password}" : "not set");
            table.AddRow($@"Smtp Url", settings.SmtpUrl != null ? $@"{settings.SmtpUrl}" : "not set");
            table.AddRow($@"Smtp Port", settings.Port != 0 ? $@"{settings.Port}" : "not set");
            table.AddRow($@"Twilio Account Sid", settings.TwilioAccountSid != null ? $@"{settings.TwilioAccountSid}" : "not set");
            table.AddRow($@"Twilio Auth Token", settings.TwilioAuthToken != null ? $@"{settings.TwilioAuthToken}" : "not set");
            table.AddRow($@"Twilio Phone Number", settings.TwilioNumber != null ? $@"{settings.TwilioNumber}" : "not set");
        }
        AnsiConsole.Write(table);
    }


}
