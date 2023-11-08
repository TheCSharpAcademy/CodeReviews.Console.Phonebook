namespace PhoneBook.Console;

using System.Text.RegularExpressions;

public class Validator
{
    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (string.IsNullOrEmpty(email)) return false;

        Regex regex = new(emailPattern);
        return regex.IsMatch(email);
    }

    public static bool IsValidPhone(string phone)
    {
        string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        if (string.IsNullOrEmpty(phone)) return false;
        return Regex.IsMatch(phone, pattern);
    }
}

