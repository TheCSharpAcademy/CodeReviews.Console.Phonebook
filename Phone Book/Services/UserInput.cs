using System.Text.RegularExpressions;
using FluentValidation;
using Spectre.Console;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

public class UserInput
{
    private Validation _validation;

    public UserInput()
    {
        _validation = new Validation();
    }

    private void Header()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new
            FigletText("Phone Book")
            .Centered()
            .Color(Color.DarkTurquoise)
            );
    }

    public MainMenuOptions MainMenu()
    {
        Header();
        var input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an action:")
            .PageSize(10)
            .AddChoices(Enum.GetNames(typeof(MainMenuOptions)).ToList())
            );

        return Enum.Parse<MainMenuOptions>(input);
    }

    public Contact Add()
    {
        Header();
        var name = _validation.GetValidName("Add a contact name:");
        var emails = _validation.AddEmails();
        var phoneNumbers = _validation.AddPhoneNumber();

        var contact = new Contact { Name = name, EmailAddresses = emails, PhoneNumbers = phoneNumbers };

        return contact;
    }

    public void DisplayMultipleContacts(List<Contact> contacts)
    {
        Header();

        var table = new Table()
            .Border(TableBorder.Minimal)
            .BorderColor(Color.LightCyan1)
            ;

        table.AddColumn("[Green]Name[/]");
        table.AddColumn("[Green]Phone Numbers[/]");
        table.AddColumn("[Green]Email Addresses[/]");
        table.Expand();

        foreach (var contact in contacts)
        {
            var phoneNumbers = contact.PhoneNumbers.Select(n => n.Number);
            var emailAddresses = contact.EmailAddresses.Select(e => e.EmailAddress);
            table.AddRow(contact.Name, string.Join("\n", phoneNumbers), string.Join("\n", emailAddresses));
        }

        AnsiConsole.Write(table);
    }


}
