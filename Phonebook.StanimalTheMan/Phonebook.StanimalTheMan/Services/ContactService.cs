using Phonebook.StanimalTheMan.Controllers;
using Phonebook.StanimalTheMan.Models;
using Phonebook.StanimalTheMan.Utils;
using Spectre.Console;

namespace Phonebook.StanimalTheMan.Services;

internal class ContactService
{
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
}
