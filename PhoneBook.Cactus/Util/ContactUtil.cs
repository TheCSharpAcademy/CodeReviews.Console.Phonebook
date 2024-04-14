using System.Text.RegularExpressions;

namespace PhoneBook.Cactus.Util;

public class ContactUtil
{
    public static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\d{3}-\d{3}-\d{4}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(phoneNumber);
    }
}

