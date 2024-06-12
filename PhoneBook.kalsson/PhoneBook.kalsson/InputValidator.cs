using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhoneBook.kalsson;

public class InputValidator
{
    public static bool ValidateEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    public static bool ValidatePhoneNumber(string phoneNumber) 
    {
        return Regex.IsMatch(phoneNumber, @"^\d{10}$");
    }
}