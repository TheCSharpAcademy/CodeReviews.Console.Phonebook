using System.Text.RegularExpressions;

namespace PhoneBook.Services;

/// <summary>
/// Provides methods for validating email addresses and phone numbers.
/// </summary>
internal static partial class ValidationService
{
    /// <summary>
    /// Validates whether the provided input string is a valid email address.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>
    /// Returns <c>true</c> if the input string is a valid email address; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidEmail(string input) =>
        EmailValidationRegex().IsMatch(input);

    /// <summary>
    /// Validates if the given input string is a valid phone number.
    /// </summary>
    /// <param name="input">The phone number string to validate.</param>
    /// <returns>
    /// Returns <c>true</c> if the input string matches the phone number pattern;
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool IsValidPhoneNumber(string input) =>
        PhoneValidationOption().IsMatch(input);
    
    
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{1,}$")]
    private static partial Regex EmailValidationRegex();
    
    [GeneratedRegex(@"^\+?\s*\d{10,12}$")]
    private static partial Regex PhoneValidationOption();
}