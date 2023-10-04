using System.Text.RegularExpressions;

public class Validator
{
    internal static bool ValidateEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }

    internal static bool ValidatePhoneNumber(string phoneNumber)
    {
        foreach (char c in phoneNumber)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }
}