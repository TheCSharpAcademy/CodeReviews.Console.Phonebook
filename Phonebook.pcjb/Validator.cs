namespace PhoneBook;

using System.Text.RegularExpressions;

class Validator
{
    // https://stackoverflow.com/questions/201323/how-can-i-validate-an-email-address-using-a-regular-expression
    public static string EmailPattern { get; } = "^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$";

    // https://stackoverflow.com/questions/123559/how-to-validate-phone-numbers-using-regex
    // https://stackoverflow.com/questions/3350500/international-phone-number-max-and-min
    public static string PhoneNumberPattern { get; } = "^[0-9 ()+-]{5,20}$";

    public static bool IsValidEmail(string? value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return false;
        }
        return Regex.IsMatch(value, EmailPattern, RegexOptions.None, TimeSpan.FromMilliseconds(250));
    }

    public static bool IsValidPhoneNumber(string? value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return false;
        }
        return Regex.IsMatch(value, PhoneNumberPattern, RegexOptions.None, TimeSpan.FromMilliseconds(250));
    }
}