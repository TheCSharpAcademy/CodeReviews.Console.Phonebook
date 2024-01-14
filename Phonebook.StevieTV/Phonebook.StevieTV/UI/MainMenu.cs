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
            menuSelection.AddChoice(MainMenuOptions.UpdateContact);
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

    private static void UpdateContact()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Update Contact")
            .Color(Color.Yellow));

        var selectedContact = SelectContact();

        if (selectedContact.Id != 0)
        {
            var finishedUpdating = false;

            while (!finishedUpdating)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("Updating Contact")
                    .Color(Color.Yellow));
                
                var table = new Table();
                table.AddColumns("Name", "E-mail", "Phone Number");
                table.AddRow(selectedContact.Name, selectedContact.Email, selectedContact.Phone);
                AnsiConsole.Write(table);

                var fieldMenu = new SelectionPrompt<UpdateContactOptions>();
                fieldMenu.Title("Please choose an option");
                fieldMenu.AddChoice(UpdateContactOptions.Name);
                fieldMenu.AddChoice(UpdateContactOptions.Email);
                fieldMenu.AddChoice(UpdateContactOptions.Phone);
                fieldMenu.AddChoice(UpdateContactOptions.Save);

                var fieldOption = AnsiConsole.Prompt(fieldMenu);

                switch (fieldOption)
                {
                    case UpdateContactOptions.Name:
                        selectedContact.Name = GetName(selectedContact.Name);
                        break;
                    case UpdateContactOptions.Email:
                        selectedContact.Email = GetEmail(selectedContact.Email);
                        break;
                    case UpdateContactOptions.Phone:
                        selectedContact.Phone = GetPhone(selectedContact.Phone);
                        break;
                    case UpdateContactOptions.Save:
                        finishedUpdating = true;
                        break;
                }
            }
            
            PhoneBookController.UpdateContact(selectedContact);
        }
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
    
    private static string GetName(string currentName = "")
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Name:")
            .DefaultValue(currentName));
    }

    private static string GetEmail(string currentEmail = "")
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Email <user@domain.tld>:")
            .Validate(input => EmailValidator.Validate(input), "Please enter a valid email <user@domain.tld>:")
            .DefaultValue(currentEmail));
    }

    private static string GetPhone(string currentPhone = "")
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Phone <+31 12345678 or 012 345678>:")
            .Validate(input => (PhoneNumber.TryParse(input, out PhoneNumber _) || PhoneNumber.TryParse(input, out IEnumerable<PhoneNumber> _)),
                "Please enter a valid phone number <+31 12345678 or 012 345678>:")
            .DefaultValue(currentPhone));
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