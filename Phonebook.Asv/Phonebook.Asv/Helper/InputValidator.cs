using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phonebook.Helper;

internal class InputValidator
{
    internal static bool IsValidEmail(string email)
    {
        if (!new EmailAddressAttribute().IsValid(email))
            return false;
        return Regex.IsMatch(email,
            @"^(?=.{1,256}$)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
    }

    internal static bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{1,14}$");
    }
}
