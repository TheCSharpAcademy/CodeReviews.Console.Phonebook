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
        if (number.Length is > 15 or < 7)
        {
            return false;
        }
        
        foreach (var c in number)
        {
            if (c is < '0' or > '9')
                return false;
        }

        return true;
    }
    
}