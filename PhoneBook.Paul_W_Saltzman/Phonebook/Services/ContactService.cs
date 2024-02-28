using Phonebook.Controllers;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Services;

internal class ContactService
{
    internal static void AddContact()
    {
        Contact newContact = new Contact();
        newContact.FirstName = AnsiConsole.Ask<string>("First Name:");
        newContact.LastName = AnsiConsole.Ask<string>("Last Name:");
        newContact = ContactController.AddContact(newContact);

        UserInterface.ShowSingleContact(newContact);
    }


    static internal Contact GetContactOptionInput()
    {

        List<Contact> contacts = ContactController.GetContacts();

        var contactArray = contacts
            .Select(x => $"{x.ContactId} {x.FirstName} {x.LastName}").ToArray();

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactArray));

        var selectedContact = contacts.FirstOrDefault(x => $"{x.ContactId} {x.FirstName} {x.LastName}" == option);

        return selectedContact;

    }

    internal static Contact UpdateContact(Contact contact)
    {
        contact.FirstName = AnsiConsole.Confirm("Update First Name?")
            ? contact.FirstName = AnsiConsole.Ask<string>("Contacts First Name:")
            : contact.FirstName;
        contact.LastName = AnsiConsole.Confirm("Update Last Name?")
            ? contact.LastName = AnsiConsole.Ask<string>("Contacts Last Name:")
            : contact.LastName;
        ContactController.UpdateContact(contact);
        return contact;
    }

}