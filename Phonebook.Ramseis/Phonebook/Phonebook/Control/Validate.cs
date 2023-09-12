using System.Text.RegularExpressions;

namespace Phonebook;

internal class Validate
{
    internal static bool Name(string name)
    {
        return Controller.GetContactName(name) == null;
    }

    private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");
    internal static string Phone(string phone)
    {
        phone = rxNonDigits.Replace(phone, "");
        if (phone.Length == 10)
        {
            phone = phone.Insert(3, "-");
            phone = phone.Insert(7, "-");
            return phone;
        }
        return "";
    }

    internal static bool Zip (int zip)
    {
        if (zip.ToString().Length == 5)
        {
            return true;
        }
        return false;
    }

    internal static bool Email (string email)
    {
        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
