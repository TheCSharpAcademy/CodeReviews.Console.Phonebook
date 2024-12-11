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

        // Single regex for email validation
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        // Validate email format
        return emailRegex.IsMatch(inputEmail) && inputEmail.Length <= 320;
    }

    internal static bool IsPhoneNumberValid(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input.Length != 12) return false;
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