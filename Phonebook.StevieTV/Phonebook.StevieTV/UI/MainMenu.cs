using PhoneBook.StevieTV.Database;
using Phonebook.StevieTV.Helpers;
using PhoneBook.StevieTV.Models;
using Spectre.Console;

namespace Phonebook.StevieTV.UI;

public class MainMenu
{
    private static PhoneBookController _phoneBookController = new();

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
        var contacts = _phoneBookController.GetContacts();

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
        var name = AnsiConsole.Ask<string>("Name:");
        var email = AnsiConsole.Ask<string>("Email:");
        var phone = AnsiConsole.Ask<string>("Phone:");

        _phoneBookController.AddContact(new Contact {Name = name, Email = email, Phone = phone});
    }

    private static void DeleteContact()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Delete Contact")
            .Color(Color.Red));

        var contacts = _phoneBookController.GetContacts();

        var deleteOptions = new SelectionPrompt<Contact>();
        deleteOptions.AddChoice(new Contact {Id = 0});
        deleteOptions.AddChoices(contacts);
        deleteOptions.UseConverter(contact => (contact.Id == 0 ? "CANCEL" : $"{contact.Name} - {contact.Email} - {contact.Phone}"));

        var selectedContact = AnsiConsole.Prompt(deleteOptions);

        if (selectedContact.Id != 0)
        {
            _phoneBookController.DeleteContact(selectedContact);
        }
        
    }
    
}