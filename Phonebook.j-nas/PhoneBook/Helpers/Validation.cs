using System.Net.Mail;
using System.Text.RegularExpressions;
using Spectre.Console;

namespace PhoneBook.Helpers;

internal static partial class Validation
{
    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    internal static string GetValidEmail()
    {
        var email = AnsiConsole.Ask<string>("Enter contact email address");
        while (!IsValidEmail(email))
            email = AnsiConsole.Ask<string>(
                "Invalid email address. Please try again.");

        return email;
    }

    private static bool IsValidPhone(string phone)
    {
        return PhoneRegex().IsMatch(phone);
    }

    internal static string GetValidPhone()
    {
        var phone =
            AnsiConsole.Ask<string>(
                "Enter contact phone number(Format: 123-456-7890)");
        while (!IsValidPhone(phone))
            phone = AnsiConsole.Ask<string>(
                "Invalid phone number. Please try again. (Format: 123-456-7890)");

        return phone;
    }

    [GeneratedRegex(@"^\d{3}-\d{3}-\d{4}$")]
    private static partial Regex PhoneRegex();
}