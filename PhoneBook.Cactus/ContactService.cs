using Spectre.Console;

namespace PhoneBook.Cactus;

public class ContactService
{
    public static void InsertContact()
    {
        var name = AnsiConsole.Ask<string>("Person's name:");
        var email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        while (!ContactUtil.IsValidEmail(email))
        {
            Console.WriteLine("Please input a valid email.");
            email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        }
        var phoneNumber = AnsiConsole.Ask<string>("Person's phone number:");

        ContactController.AddContact(new Contact { Name = name, Email = email, PhoneNumber = phoneNumber });

        UserInterface.BackToMainMenuPrompt();
    }

    public static void DeleteContact()
    {
        var contact = GetContactOptionInput();
        ContactController.DeleteContact(contact);
    }

    public static void ShowAllContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContacts(contacts);
    }

    public static void ShowSpecificContact()
    {
        Contact contact = GetContactOptionInput();
        UserInterface.ShowContact(contact);
    }

    public static void UpdateContact()
    {
        var contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name?") ? contact.Name : AnsiConsole.Ask<string>("Product's new name:");
        contact.Email = AnsiConsole.Confirm("Update email?") ? contact.Name : AnsiConsole.Ask<string>("Product's new email (format xxx@yyy.zzz):");
        while (!ContactUtil.IsValidEmail(contact.Email))
        {
            Console.WriteLine("Please input a valid email.");
            contact.Email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        }
        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?") ? contact.Name : AnsiConsole.Ask<string>("Product's new phone number:");

        ContactController.UpdateContact(contact);
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
