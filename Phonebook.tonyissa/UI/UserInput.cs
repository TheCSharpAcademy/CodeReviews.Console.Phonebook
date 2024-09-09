using Spectre.Console;
using System.Text.RegularExpressions;

namespace Phonebook.tonyissa.UI;

public static class UserInputHandler
{
    public static string GetName(string prompt)
    {
        return AnsiConsole.Ask<string>(prompt);
    }

    public static string GetEmail()
    {
        try
        {
            var email = AnsiConsole.Ask<string>("Please enter an email for this contact");
            if (email.ToLower() == "quit") return email;

            var splitString = email.Split('@');

            if (splitString.Length != 2
                || !splitString[1].Contains('.')
                || splitString[1].Substring(splitString.Length - 1) == "."
                || splitString[1].StartsWith('.')
                || email.Contains(' '))
            {
                throw new ArgumentException("Invalid email address format");
            }

            return email;
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            return GetEmail();
        }
    }

    public static string GetNumber()
    {
        try
        {
            var phoneNumber = AnsiConsole.Ask<string>("Please enter a valid phone number in the (X-)XXX-XXX-XXXX format");
            var regex = "^\\d?-?\\d{3}-\\d{3}-\\d{4}$";
            
            if (!Regex.IsMatch(phoneNumber, regex) && phoneNumber.ToLower() != "quit")
            {
                throw new ArgumentException("Invalid phone number format");
            }

            return phoneNumber;
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            return GetNumber();
        }
    }

    public static int GetContactPosFromList(List<Contact> contacts)
    {
        try
        {
            var input = AnsiConsole.Ask<int>("Enter the ID for the contact you want to select");

            if (input < 1 || input > contacts.Count)
            {
                throw new ArgumentOutOfRangeException(null, "Argument out of range. Try again");
            }

            return input - 1;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            return GetContactPosFromList(contacts);
        }
    }
}