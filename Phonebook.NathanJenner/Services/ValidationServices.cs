using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Console.Phonebook.App.Services;

internal class ValidationServices
{
    public static bool IsValidEmailAddress(string inputEmail)
    {
        var email = new EmailAddressAttribute();
        return email.IsValid(inputEmail);
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        Regex numberRegex = new Regex(@"^([0-9]{9})$");
        Regex phoneRegex = new Regex(@"^([0-9]{4}\s[0-9]{3}\s[0-9]{3})$");

        if (numberRegex.IsMatch(phoneNumber) || phoneRegex.IsMatch(phoneNumber))
        {
            return true;
        }
        return false;
    }
}
