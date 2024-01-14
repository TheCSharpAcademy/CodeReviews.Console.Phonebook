using EmailValidation;
using PhoneBook.StevieTV.Database;
using Phonebook.StevieTV.Helpers;
using PhoneBook.StevieTV.Models;
using PhoneNumbers;
using Spectre.Console;

namespace Phonebook.StevieTV.UI;

public static class MainMenu
{
    private static readonly PhoneBookController PhoneBookController = new();

    public static void ShowMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(new FigletText("Phone Book")
                .Color(Color.Blue));

            var menuSelection = new SelectionPrompt<MainMenuOptions>();
            menuSelection.Title("Please choose an option");
            menuSelection.AddChoice(MainMenuOptions.ViewContacts);
            menuSelection.AddChoice(MainMenuOptions.AddContact);
            menuSelection.AddChoice(MainMenuOptions.DeleteContact);
            menuSelection.AddChoice(MainMenuOptions.Exit);
            menuSelection.UseConverter(option => option.GetEnumDescription());

            var selectedOption = AnsiConsole.Prompt(menuSelection);

            switch (selectedOption)
            {
                case MainMenuOptions.ViewContacts:
                    ShowContacts();
                    break;
                case MainMenuOptions.AddContact:
                    AddContact();
                    break;
                case MainMenuOptions.DeleteContact:
                    DeleteContact();
                    break;
                case MainMenuOptions.UpdateContact:
                    UpdateContact();
                    break;
                case MainMenuOptions.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static void ShowContacts()
    {
        var contacts = PhoneBookController.GetContacts();

        var table = new Table();
        table.AddColumns("Name", "E-mail", "Phone Number");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.Email, contact.Phone);
        }

        AnsiConsole.Write(table);

        AnsiConsole.Prompt(new ConfirmationPrompt("Press enter to continue"));
    }

    private static void AddContact()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(new FigletText("Add Contact")
            .Color(Color.Green));

        var name = GetName();
        var email = GetEmail();
        var phone = GetPhone();

        PhoneBookController.AddContact(new Contact {Name = name, Email = email, Phone = phone});
    }

    private static void DeleteContact()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(new FigletText("Delete Contact")
            .Color(Color.Red));

        var selectedContact = SelectContact();

        if (selectedContact.Id != 0)
        {
            PhoneBookController.DeleteContact(selectedContact);
        }
    }

    private static void UpdateContact()
    {
    }

    private static string GetName()
    {
        return AnsiConsole.Ask<string>("Name:");
    }

    private static string GetEmail()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Email <user@domain.tld>:")
            .Validate(input => EmailValidator.Validate(input), "Please enter a valid email <user@domain.tld>:"));
    }

    private static string GetPhone()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Phone <+31 12345678 or 012 345678>:")
            .Validate(input => (PhoneNumber.TryParse(input, out PhoneNumber _) || PhoneNumber.TryParse(input, out IEnumerable<PhoneNumber> _)),
                "Please enter a valid phone number <+31 12345678 or 012 345678>:"));
    }

    private static Contact SelectContact()
    {
        var contacts = PhoneBookController.GetContacts();

        var selectOptions = new SelectionPrompt<Contact>();
        selectOptions.AddChoice(new Contact {Id = 0});
        selectOptions.AddChoices(contacts);
        selectOptions.UseConverter(contact => (contact.Id == 0 ? "CANCEL" : $"{contact.Name} - {contact.Email} - {contact.Phone}"));

        var selectedContact = AnsiConsole.Prompt(selectOptions);

        return selectedContact;
    }
}