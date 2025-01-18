using Spectre.Console;
using System.Text.RegularExpressions;

internal partial class ContactCreate
{
    internal static void Create()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[yellow]Creating new contact.[/]\n");

        var (name, phone, email) = GetContactInput();

        var contact = new Contact
        {
            Name = name,
            Phone = phone,
            Email = email
        };

        var createContact = new Action(() =>
        {
            using var database = new ContactContext();
            database.Contacts.Add(contact);
            database.SaveChanges();
        });

        if (!ErrorHandler.Success(createContact)) return;
        AnsiConsole.MarkupLine("[green]A new contact added successfully![/]");
        DisplayInfoHelpers.PressAnyKeyToContinue();
    }

    internal static (string, string, string) GetContactInput()
    {
        var name = GetName();
        var phone = GetPhone();
        var email = GetEmail();
        return (name, phone, email);
    }

    private static string GetName()
    {
        var name = AnsiConsole.Ask<string>("Enter contact's name:");
        while (name.Length < 3)
        {
            AnsiConsole.MarkupLine("[red]Contact name should be at least 3 characters long.[/]");
            name = AnsiConsole.Ask<string>("Enter contact's name:");
        }
        return name;
    }

    private static string GetPhone()
    {
        string phone = AnsiConsole.Ask<string>("Enter contact's phone number in format '+1 123 456 7890:");
        while (!Phone().IsMatch(phone))
        {
            AnsiConsole.MarkupLine("[red]Contact phone should match '+1 123 456 7890' format.[/]");
            phone = AnsiConsole.Ask<string>("Enter phone number:");
        }
        return phone;
    }

    private static string GetEmail() 
    {
        string email = AnsiConsole.Ask<string>("Enter contact's email address:");
        while (!Email().IsMatch(email))
        {
            AnsiConsole.MarkupLine("[red]Entered email invalid.[/]");
            email = AnsiConsole.Ask<string>("Enter email:");
        }
        return email;
    }

    [GeneratedRegex(@"^\+?([0-9]{1})[ ]([0-9]{3})[ ]([0-9]{3})[ ]([0-9]{4})$")]
    private static partial Regex Phone();

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex Email();
}
