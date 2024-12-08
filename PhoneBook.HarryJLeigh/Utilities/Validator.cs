using System.Text.RegularExpressions;

namespace Phonebook.Utilities;

public static class Validator
{
    internal static bool IsNameValid(string inputName)
    {
        if (HasDigit(inputName)) return false;
        return true;
    }

    internal static bool IsEmailValid(string inputEmail)
    {
        if (string.IsNullOrWhiteSpace(inputEmail)) return false;

        var parts = inputEmail.Split('@');
        if (parts.Length != 2) return false;

        var localPart = parts[0];
        var domainPart = parts[1];

        // Check local part constraints
        if (localPart.Length > 64 || string.IsNullOrWhiteSpace(localPart))
            return false;
        if (domainPart.Length > 255 || string.IsNullOrWhiteSpace(domainPart))
            return false;
        // Domain must contain a dot and no invalid characters
        if (!domainPart.Contains('.') || domainPart.StartsWith("-") || domainPart.EndsWith("-"))
            return false;

        var localRegex = new Regex(@"^[a-zA-Z0-9._%+-]+$");
        var domainRegex = new Regex(@"^[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        return localRegex.IsMatch(localPart) && domainRegex.IsMatch(domainPart);
    }

    internal static bool IsPhoneNumberValid(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input.Length != 12 ) return false;
        if (input[0] != '+') return false;
        if (HasLetter(input)) return false;
        
        return true;
    }

    internal static bool IsCategoryValid(string input)
    {
        if (input.ToLower() == "friends") return true;
        if (input.ToLower() == "family") return true;
        if (input.ToLower() == "work") return true;

        return false;
    }

    private static bool HasDigit(string input)
    {
        bool result = false;
        foreach (char c in input)
        {
            if (char.IsDigit(c)) result = true;
        }
        return result;
    }

    private static bool HasLetter(string input)
    {
        string inputSlice = input[1..];
        return inputSlice.Any(c => char.IsLetter(c));
    }
}