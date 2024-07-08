using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Phonebook.Helper;

internal class InputValidator
{
    internal static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("The email address is empty or contains only whitespace.");
            return false;
        }
        if (email.Length > 256)
        {
            Console.WriteLine("The email address is too long.");
            return false;
        }
        if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
        {
            Console.WriteLine("The email address format is invalid.");
            return false;
        }
        if (!new EmailAddressAttribute().IsValid(email))
        {
            Console.WriteLine("The email address failed the EmailAddressAttribute validation.");
            return false;
        }
        return true;
    }

    internal static bool IsValidPhoneNumber(string phoneNumber)
    {
        string pattern = @"^\+\d{1,3}\d{9,11}$";
        Match match = Regex.Match(phoneNumber, pattern);
        if (!phoneNumber.StartsWith("+"))
        {
            Console.WriteLine("Error: Phone number must start with a '+' sign.");
            return false;
        }
        else if (phoneNumber.Length < 12 || phoneNumber.Length > 14)
        {
            Console.WriteLine("Error: Phone number length is incorrect.");
            return false;
        }
        else if (!match.Success)
        {
            Console.WriteLine("Error: Phone number contains invalid characters or whitespaces. Only digits and '+' for the initial country code are allowed.");
            return false;
        }
        else
            return true;
    }

    internal static bool IsGivenInputInteger(string? input)
    {
        if (int.TryParse(input, out _))
            return true;
        else
            return false;
    }
}