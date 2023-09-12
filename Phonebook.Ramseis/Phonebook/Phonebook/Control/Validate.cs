using System.Text.RegularExpressions;

namespace Phonebook;

internal class Validate
{
    internal static bool Name(string input)
    {
        return Controller.GetContactName(input) == null;
    }

    private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");
    internal static string Phone(string input)
    {
        input = rxNonDigits.Replace(input, "");
        if (input.Length == 10)
        {
            input = input.Insert(3, "-");
            input = input.Insert(7, "-");
            return input;
        }
        return "";
    }

    internal static bool Zip (int input)
    {
        if (input.ToString().Length == 5)
        {
            return true;
        }
        return false;
    }

    internal static bool Email (string input)
    {
        try
        {
            return Regex.IsMatch(input,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
