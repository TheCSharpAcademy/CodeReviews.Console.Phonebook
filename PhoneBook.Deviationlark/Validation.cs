using System.Net.Mail;
using PhoneNumbers;

namespace PhoneBook;

internal static class Validation
{
    // Peeked at dejmen's code for this, Cheers Dejmen
    private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
    internal static bool ValidateNumber(string phoneNumber)
    {
        bool valid = false;
        try
        {
            var number = phoneNumberUtil.Parse(phoneNumber, null);
            if (phoneNumberUtil.IsValidNumber(number)) valid = true;
            return valid;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Press Enter to try again.");
            Console.ReadLine();
        }
        return valid;

    }
    internal static bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    internal static bool ValidateCategory(string? option)
    {
        var isNumber = int.TryParse(option, out _);
        return isNumber;
    }

    internal static bool ValidateString(string userInput, List<Contact> contacts)
    {
        var isNull = string.IsNullOrEmpty(userInput);
        if (isNull)
        {
            Console.WriteLine("Invalid Id! Try again.");
            return isNull;
        }
        foreach (var contact in contacts)
        {
            var isNum = int.TryParse(userInput, out _);
            if (isNum == false)
            {
                Console.WriteLine("That's not a number! Try again.");
                return true;
            }
            if (int.Parse(userInput) == contact.Id)
            {
                isNull = false;
                break;
            }
            else
                isNull = true;
        }
        if (isNull)
        {
            {
                Console.WriteLine("Invalid Id. Try again.");
                return true;
            }
        }
        return isNull;
    }
}