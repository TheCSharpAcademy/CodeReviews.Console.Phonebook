using System.Net.Mail;
using PhoneNumbers;

namespace PhoneBook;

internal static class Validation
{
    // Peeked at dejmen's code for this, Cheers Dejmen
    private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
    internal static bool ValidateNumber(string phoneNumber)
    {
        var number = phoneNumberUtil.Parse(phoneNumber, null);
        bool valid = false;
        if (phoneNumberUtil.IsValidNumber(number)) valid = true;
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

    internal static bool ValidateString(string name)
    {
        var isNull = string.IsNullOrEmpty(name);
        return isNull;
    }
}