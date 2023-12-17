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
        Regex phonePattern = new Regex(@"\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*");
        return phonePattern.Match(number).Success;
    }
    
}