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

    public T Menu<T>(string title) where T : struct, Enum
    {
        Header();
        var input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(title)
            .PageSize(10)
            .AddChoices(Enum.GetNames(typeof(T)).ToList())
            );

        return Enum.Parse<T>(input);
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

    public string UpdateName() => _validation.GetValidName("Type a new name for this contact:");

    public string UpdateNumber() => _validation.GetValidPhoneNumber("Type a new number:");

    public string UpdateEmail() => _validation.GetValidEmail("Type a new e-mail address:");

    public T SelectItemFromList<T>(List<T> numbers, string title) where T : class
    {
        Header();
        return AnsiConsole.Prompt(
            new SelectionPrompt<T>()
            .Title(title)
            .PageSize(10)
            .AddChoices(numbers)
            );
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

    public (string Title, string Body) SendMessage()
    {
        var title = AnsiConsole.Ask<string>("Give your email a title");
        var body = AnsiConsole.Ask<string>("Type a body for your email");
        var email = (Title: title, Body: body);
        return email;
    }

    private void PauseAndWaitForUserInput()
    {
        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey(true);
    }

    public void NoRecords()
    {
        Console.Beep();
        AnsiConsole.MarkupLine("[red]Cannot access this option as insufficient records could be found for this option.[/]");
        AnsiConsole.MarkupLine("Please add records before access this option.");
        PauseAndWaitForUserInput();
    }

    public void InvalidName()
    {
        Console.Beep();
        AnsiConsole.MarkupLine("[red]Cannot insert or update this record, as this name already exists in our records. Names must be unique.[/]");
        AnsiConsole.MarkupLine("Please add records before access this option.");
        PauseAndWaitForUserInput();
    }
}
