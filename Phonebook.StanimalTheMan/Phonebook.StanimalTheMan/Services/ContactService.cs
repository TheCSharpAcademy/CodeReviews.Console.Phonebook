using Phonebook.StanimalTheMan.Controllers;
using Phonebook.StanimalTheMan.Models;
using Phonebook.StanimalTheMan.Utils;
using Spectre.Console;

namespace Phonebook.StanimalTheMan.Services;

internal class ContactService
{
    internal static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        ContactController.DeleteContact(contact);
    }

    internal static void GetContact()
    {
        var contact = GetContactOptionInput();
        UserInterface.ShowContact(contact);
    }

    internal static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContactTable(contacts);
    }

    internal static void InsertContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Contact's name:"),
            Email = Validator.GetEmail(AnsiConsole.Ask<string>("Contact's email:")),
            PhoneNumber = Validator.GetPhoneNumber(AnsiConsole.Ask<string>("Contact's phone number:"))
        };

        ContactController.AddContact(contact);
    }

    private static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(contact => contact.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(contact => contact.Name == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }
}
