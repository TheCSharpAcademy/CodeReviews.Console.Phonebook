using PhoneNumbers;

namespace PhoneBook;

public class Validate
{
    public static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith('.'))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhoneNumber(string number)
    {
        bool isValid;

        try
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phoneNumber = phoneNumberUtil.Parse(number, null);
            isValid = phoneNumberUtil.IsValidNumber(phoneNumber);
        }
        catch
        {
            isValid = false;
        }

        return isValid;
    }
}