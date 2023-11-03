namespace PhoneBook.Console;

using System.Net.Mail;

public class Validator
{
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        try
        {
            var addr = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhone(string phone)
    {
        var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
        try
        {
            PhoneNumbers.PhoneNumber number = phoneNumberUtil.Parse(phone, "US");
            return phoneNumberUtil.IsValidNumber(number);
        }
        catch (PhoneNumbers.NumberParseException _)
        {
            return false;
        }
    }
}
