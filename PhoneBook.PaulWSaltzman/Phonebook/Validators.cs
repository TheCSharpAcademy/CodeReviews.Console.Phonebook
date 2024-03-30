using Phonebook.Models;
using PhoneNumbers;
using System.Text.RegularExpressions;

namespace Phonebook;

internal class Validators
{

    internal static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }
    internal static bool IsValidSmtpUrl(string smtpUrl)
    {
        string pattern = @"^smtp\.[a-zA-Z0-9-]+\.[a-zA-Z0-9]+(\.[a-zA-Z]{2,})?(/[a-zA-Z0-9._~-]*)*$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(smtpUrl);
    }

    internal static bool Duplicate(string emailAddress, ICollection<Email> emails)
    {
        return emails.Any(email => email.EmailAddress == emailAddress);
    }

    internal static bool IsDuplicatePhone(PhoneNumber newPhone, ICollection<Phone> Phones)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        try
        {
            return Phones.Any(existingPhone =>
                phoneUtil.IsNumberMatch(ParsePhoneNumber(existingPhone.PhoneNumber), newPhone) != PhoneNumberUtil.MatchType.NO_MATCH);
        }
        catch (NumberParseException ex)
        {
            Console.WriteLine($"Comparing numbers {ex.Message}");
            return true;
        }
    }


    internal static PhoneNumber ParsePhoneNumber(string userInput)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        if (userInput[0] == '+')
        {
            //do nothing 
        }
        else
        {
            userInput = "+" + userInput;
        }
        try
        {
            PhoneNumber parsedPhoneNumber = phoneUtil.Parse(userInput, null);
            return parsedPhoneNumber;
        }
        catch (NumberParseException ex)
        {
            Console.WriteLine($"Error parsing phone number: {ex.Message}");
            // Return null or rethrow the exception based on your application's needs
            return null;
        }
    }
    internal static bool IsValidPhone(PhoneNumber newPhone)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
        try
        {
            bool isValid = phoneUtil.IsValidNumber(newPhone);
            return isValid;
        }
        catch (NumberParseException ex)
        {
            Console.WriteLine($"Error validating phone number: {ex.Message}");
            return false;
        }

    }

    internal static bool InvalidPassword(string password0, string password1)
    {
        return password0.Trim() == password1.Trim();
    }

    internal static string FormatNumber(PhoneNumber parsedPhoneNumber)
    {
        PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();

        string formattedPhoneNumber = phoneUtil.Format(parsedPhoneNumber, PhoneNumberFormat.INTERNATIONAL);
        return formattedPhoneNumber;

    }
}
