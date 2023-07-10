using Spectre.Console;
using System.Text.RegularExpressions;

namespace Kmakai.PhoneBook;

public class inputValidation
{
    public static bool IsValidName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty");
            return false;
        }

        if (name.Length > 50)
        {
            Console.WriteLine("Name cannot be longer than 50 characters");
            return false;
        }

        if (name.Length < 2)
        {
            Console.WriteLine("Name cannot be shorter than 2 characters");
            return false;
        }

        foreach (char c in name)
        {
            if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
            {
                Console.WriteLine("Name can only contain letters and spaces");
                return false;
            }
        }


        return true;
    }

    public static string GetAndValidateName()
    {
        var name = AnsiConsole.Ask<string>("Enter contact name:");
        while (!IsValidName(name))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid name![/]");
            name = AnsiConsole.Ask<string>("Enter contact name:");
        }

        return name;
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        Regex numberRegex = new Regex(@"^([0-9]{9})$");
        Regex phoneRegex = new Regex(@"^([0-9]{3}\s[0-9]{3}\s[0-9]{4})$");

        if (numberRegex.IsMatch(phoneNumber) || phoneRegex.IsMatch(phoneNumber))
        {
            return true;
        }

        return false;

    }

    public static string GetAndValidateNumber()
    {
        var phoneNumber = AnsiConsole.Ask<string>("\"Enter contact phone number eg. 123 456 7890:");
        while (!IsValidPhoneNumber(phoneNumber))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid phone number![/]");
            phoneNumber = AnsiConsole.Ask<string>("Enter contact phone number eg. 123 456 7890:");
        }

        return phoneNumber;
    }

    public static bool IsValidEmail(string email)
    {
        Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (emailRegex.IsMatch(email))
        {
            return true;
        }

        return false;
    }

    public static string GetAndValidateEmail()
    {
        var email = AnsiConsole.Ask<string>("Enter contact email:");
        while (!IsValidEmail(email))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid email![/]");
            email = AnsiConsole.Ask<string>("Enter contact email:");
        }

        return email;
    }
}
