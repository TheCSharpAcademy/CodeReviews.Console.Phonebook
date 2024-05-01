using Phonebook.samggannon.Controllers;
using Phonebook.samggannon.Models;
using Spectre.Console;

namespace Phonebook.samggannon.Services;

internal class ContactsService
{
    internal static void AddContact()
    {
        var contact = new Contact();
        contact.Name = AnsiConsole.Ask<string>("Enter a name for your contact *Required: ");
        contact.Email = AnsiConsole.Ask<string>("Enter an email for your contact *optional: ");
        contact.PhoneNumber = AnsiConsole.Ask<string>("Enter a phone number for your contact *Required: ");

        ContactsController.AddContact(contact);
    }
}
