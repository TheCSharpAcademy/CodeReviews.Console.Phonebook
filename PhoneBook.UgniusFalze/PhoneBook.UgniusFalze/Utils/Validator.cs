using System.Text.RegularExpressions;
using EmailValidation;

namespace PhoneBook.UgniusFalze.Utils;

public static class Validator
{
    public static bool IsValidEmail(string email)
    {
        return EmailValidator.Validate(email);
    }

    public static bool IsValidPhoneNumber(string number)
    {
        if (number.Length > 15 || number.Length < 7)
        {
            return false;
        }
        
        foreach (char c in number)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }
    
}