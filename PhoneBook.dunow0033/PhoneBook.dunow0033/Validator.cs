using System.Text.RegularExpressions;

namespace PhoneBook.dunow0033;

public class Validator
{
    internal static bool ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }

    internal static bool ValidatePhoneNumber(string phoneNumber)
    {
        string phoneNumberPattern = "^(1-)?\\d{3}-\\d{3}-\\d{4}$";

        return Regex.IsMatch(phoneNumber, phoneNumberPattern);
    }
}
