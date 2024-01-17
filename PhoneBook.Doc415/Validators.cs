using PhoneNumbers;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PhoneBook.Doc415;

internal class Validators
{
    static Regex ValidEmailRegex = CreateValidEmailRegex();
    public static bool IsValidPhone(string phone) 
    {
        var PhoneValidator = PhoneNumberUtil.GetInstance();
        try
        {
            var phoneNumber = PhoneValidator.Parse(phone,CountrySelection.CountryCode);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    static public bool IsValidEmail(string email) 
    {
        bool isValid = ValidEmailRegex.IsMatch(email);
        if (!isValid)
            Console.WriteLine("Not a valid email");
        return isValid;
    }

    private static Regex CreateValidEmailRegex()
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
    }
}
