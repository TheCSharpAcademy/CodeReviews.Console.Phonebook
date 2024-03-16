using System.Text.RegularExpressions;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Views;

public partial class PhoneBookView : BaseView
{
    public string GetContactName(string name = "")
    {
        return AskInput("What's the contact's name?", name);
    }

    public string GetContactEmail(string email = "")
    {
        var textPrompt = new TextPrompt<string>("What's the contact's e-mail?");

        if (!string.IsNullOrEmpty(email))
            textPrompt.DefaultValue(email);
        textPrompt.Validate(x => EmailRegex().Match(x).Success, "[red]Email not valid[/]");

        return AnsiConsole.Prompt(textPrompt);
    }

    public string GetPhoneNumber(string number = "")
    {
        var formatMessage = "Phone number must be in formats xx-xxxxxxxx or xxx-xxxxxxx";
        AnsiConsole.MarkupLine($"[yellow3][bold]{formatMessage}[/][/]");

        var textPrompt = new TextPrompt<string>("What's the contact's phone number?");

        if (!string.IsNullOrEmpty(number))
            textPrompt.DefaultValue(number);
        textPrompt.Validate(ValidatePhoneNumber, $"[red]{formatMessage}[/]");

        return AnsiConsole.Prompt(textPrompt);
    }

    public void ShowTable(List<Contact> contacts)
    {
        var table = new Table();
        table.Title("Your phone book");

        table.AddColumn("Name");
        table.AddColumn("Email");
        table.AddColumn("Phone");

        contacts.ForEach(c => table.AddRow(c.Name, c.Email, c.PhoneNumber));

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[grey]Press any key to go back[/]");
        Console.ReadKey();
    }

    private bool ValidatePhoneNumber(string number)
    {
        return number.Length <= 11 && DutchPhoneRegex().Match(number).Success;
    }

    [GeneratedRegex(@"^\d{2,3}-\d{7,8}$")]
    private static partial Regex DutchPhoneRegex();

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();
}