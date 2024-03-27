using System.Text.RegularExpressions;

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
        return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
    }
}