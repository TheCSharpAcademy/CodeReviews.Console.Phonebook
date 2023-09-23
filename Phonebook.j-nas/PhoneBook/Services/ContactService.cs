using PhoneBook.Controllers;
using PhoneBook.Helpers;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Services;

internal static class ContactService
{
    public static void ListContacts()
    {
        var contact = GetContactOptionInput();
        UserInterface.ShowContactDetails(contact);
    }

    public static void InsertContact()
    {
        var contact = new Contact
        {
            Name = AnsiConsole.Ask<string>("Enter contact name"),
            Phone = Validation.GetValidPhone(),
            Email = Validation.GetValidEmail()
        };

        ContactController.AddContact(contact);
    }

    public static void SearchContacts()
    {
        Console.WriteLine("Search function not implemented yet");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name);

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a contact")
                .PageSize(10)
                .MoreChoicesText(
                    "[grey](Move up and down to reveal more contacts)[/]")
                .AddChoices(contactsArray)
        );

        var id = contacts.Single(x => x.Name == option).ContactId;
        var contact = ContactController.GetContactById(id);

        return contact;
    }

    public static void UpdateContact(Contact contact)
    {
        contact.Name = AnsiConsole.Confirm("Update contact name?")
            ? AnsiConsole.Ask<string>("Enter new contact name")
            : contact.Name;
        contact.Phone = AnsiConsole.Confirm("Update contact phone number?")
            ? Validation.GetValidPhone()
            : contact.Phone;
        contact.Email = AnsiConsole.Confirm("Update contact email address?")
            ? Validation.GetValidEmail()
            : contact.Email;

        ContactController.UpdateContact(contact);
    }

    public static void DeleteContact(Contact contact)
    {
        ContactController.DeleteContact(contact);
        Console.WriteLine("Contact deleted successfully");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}