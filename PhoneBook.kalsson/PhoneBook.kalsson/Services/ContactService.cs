using PhoneBook.kalsson.Controllers;
using PhoneBook.kalsson.Models;
using Spectre.Console;

namespace PhoneBook.kalsson.Services;

public class ContactService
{
    static internal Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetAllContacts();
        var contactsArray = contacts.Select(x => x.FirstName + x.LastName).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.FirstName + x.LastName == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }
}