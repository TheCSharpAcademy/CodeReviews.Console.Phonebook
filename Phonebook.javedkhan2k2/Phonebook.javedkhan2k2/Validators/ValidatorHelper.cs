using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Phonebook.Entities;
using Spectre.Console;

namespace Phonebook.Validators;

public static class ValidatorHelper
{
    public static bool IsValidName(string value) => Regex.IsMatch(value, @"^[a-zA-Z][a-zA-Z\s]{2,}$");

    public static bool IsValidEmail(string value) => Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public static bool IsValidPhoneNumber(string value) => Regex.IsMatch(value, @"^\+49\d{10,14}$");

    internal static bool IsValidConfiguration(IConfigurationRoot configuration)
    {
        StringBuilder errors = new StringBuilder();
        bool result = true;
        if(string.IsNullOrEmpty(configuration["DatabaseUserID"]))
        {
            errors.Append("Please Set a Valid Database UserID\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["DatabasePassword"]))
        {
            errors.Append("Please Set a Valid Database Password\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["twilioAccountSid"]))
        {
            errors.Append("Please Set a Valid Twilio Account Sid\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["twilioAuthToken"]))
        {
            errors.Append("Please Set a Valid Twilio Auth Token\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["twilioNumber"]))
        {
            errors.Append("Please Set a Valid Twilio Phone Number\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["senderEmail"]))
        {
            errors.Append("Please Set a Valid Sender Email\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["senderPassword"]))
        {
            errors.Append("Please Set a Valid Sender Email Password\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["smtpHost"]))
        {
            errors.Append("Please Set a Valid SMTP Host\n");
            result = false;
        }
        if(string.IsNullOrEmpty(configuration["smtpPort"]))
        {
            errors.Append("Please Set a Valid SMTP Port\n");
            result = false;
        }
        else if(!int.TryParse(configuration["smtpPort"], out _))
        {
            errors.Append("The SMTP Port should be a valid 3 Digit Number. e.g. 587\n");
            result = false;
        }
        if(!result)
        {
            AnsiConsole.Markup($"[green]Please Fix the Errors Below and run the application again.\n[/][maroon]{errors.ToString()}[/]");
        }
        return result;
    }
}