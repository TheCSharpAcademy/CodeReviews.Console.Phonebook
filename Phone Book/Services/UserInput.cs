using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
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

    private Table CreateContactTable(IEnumerable<Contact> contacts)
    {
        var table = new Table()
            .Border(TableBorder.Minimal)
            .BorderColor(Color.LightCyan1);

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

        return table;
    }

    public void DisplayContact(Contact contact)
    {
        Header();

        var table = CreateContactTable(new[] { contact });

        AnsiConsole.Write(table);

        PauseAndWaitForUserInput();
    }

    public void DisplayContacts(List<Contact> contacts)
    {
        Header();

        var table = CreateContactTable(contacts);

        AnsiConsole.Write(table);

        PauseAndWaitForUserInput();
    }

    public Contact PickAContact(List<Contact> contacts)
    {

        var input = AnsiConsole.Prompt(
            new SelectionPrompt<Contact>()
            .Title("Please choose a contact")
            .PageSize(10)
            .AddChoices(contacts)
            );

        return input;
    }

    private void PauseAndWaitForUserInput()
    {
        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey(true);
    }
}
