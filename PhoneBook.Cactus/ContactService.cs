using Spectre.Console;

namespace PhoneBook.Cactus;

public class ContactService
{
    public static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        ContactController.DeleteContact(contact);
    }

    public static Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).Id;
        var contact = ContactController.GetContactById(id);

        return contact;
    }

}
