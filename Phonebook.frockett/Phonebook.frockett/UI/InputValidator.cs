using System.Text.RegularExpressions;

namespace Phonebook.frockett.UI;

public class InputValidator
{
    public bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return false;

        return true;
    }
    
    // Credit to chatGPT for generating these regex patterns, I have no idea about regex syntax. I glad that I now know the power of regex for tasks like this, though.
    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(email);
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        string pattern = @"^(\d{3}-?\d{3}-?\d{4})$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(phoneNumber);
    }
}
