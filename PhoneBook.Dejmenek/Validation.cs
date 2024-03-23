using PhoneNumbers;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace PhoneBook.Dejmenek;

public static class Validation
{
    private static readonly Regex emailRegex = new Regex("^((([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+(\\.([a-z]|\\d|[!#\\$%&'\\*\\+\\-\\/=\\?\\^_`{\\|}~]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])+)*)|((\\x22)((((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(([\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x7f]|\\x21|[\\x23-\\x5b]|[\\x5d-\\x7e]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(\\\\([\\x01-\\x09\\x0b\\x0c\\x0d-\\x7f]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF]))))*(((\\x20|\\x09)*(\\x0d\\x0a))?(\\x20|\\x09)+)?(\\x22)))@((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?$");
    private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

    public static ValidationResult IsValidString(string input)
    {
        if (input.Trim().Length != 0)
        {
            return ValidationResult.Success();
        }
        else
        {
            return ValidationResult.Error("Your input must not be empty!");
        }
    }

    public static ValidationResult IsValidPhoneNumber(string userPhoneNumber)
    {
        try
        {
            var phoneNumber = phoneNumberUtil.Parse(userPhoneNumber, null);

            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            {
                return ValidationResult.Error("Provided phone number is not valid.");
            }

            return ValidationResult.Success();
        }
        catch
        {
            return ValidationResult.Error("Provided phone number is not valid.");
        }
    }

    public static ValidationResult IsValidEmail(string email)
    {
        Match emailMatch = emailRegex.Match(email);

        if (emailMatch.Success)
        {
            return ValidationResult.Success();
        }
        else
        {
            return ValidationResult.Error("The email address seems to be missing a '@' symbol or proper domain name (e.g., johndoe@example.com).");
        }
    }
}
