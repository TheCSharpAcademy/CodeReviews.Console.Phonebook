using phonebook.Fennikko.Controllers;
using phonebook.Fennikko.Models;
using Spectre.Console;

namespace phonebook.Fennikko.Services;

public class ContactInfoService
{
    public static void InsertContact()
    {
        var contact = new ContactInfo
        {
            ContactName = AnsiConsole.Ask<string>("Contact's name: "),
            ContactPhone = AnsiConsole.Ask<string>("Contact's phone number: "),
            ContactEmail = AnsiConsole.Ask<string>("Contact's email address: "),
            CategoryId = CategoryService.GetCategoryOptionInput().CategoryId
        };
        ContactInfoController.AddContact(contact);
    }

    public static void DeleteContact()
    {
        var contact = GetContactInfoOptionInput();
        ContactInfoController.DeleteContact(contact);
    }

    public static void UpdateContact()
    {
        var contact = GetContactInfoOptionInput();
        contact.ContactName = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Contact's new name: ")
            : contact.ContactName;

        contact.ContactPhone = AnsiConsole.Confirm("Update phone number?")
            ? AnsiConsole.Ask<string>("Contact's new phone number: ")
            : contact.ContactPhone;

        contact.ContactEmail = AnsiConsole.Confirm("Update email?")
            ? AnsiConsole.Ask<string>("Contact's new email: ")
            : contact.ContactEmail;

        contact.Category = AnsiConsole.Confirm("Update category?")
            ? CategoryService.GetCategoryOptionInput()
            : contact.Category;

        ContactInfoController.UpdateContact(contact);
    }

    public static void GetContacts()
    {
        var contacts = ContactInfoController.GetContacts();
        UserInterface.ShowContactTable(contacts);
    }

    public static void GetContactById()
    {
        var contact = GetContactInfoOptionInput();
        UserInterface.ShowContact(contact);
    }

    public static ContactInfo GetContactInfoOptionInput()
    {
        var contacts = ContactInfoController.GetContacts();
        var contactsArray = contacts.Select(c => c.ContactName).ToArray();
        if (!contactsArray.Any())
        {
            AnsiConsole.Write("No contacts found, press any key to return to the main menu.");
            Console.ReadKey();
            UserInterface.MainMenu();
        }

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(c => c.ContactName == option).ContactId;
        var contact = ContactInfoController.GetContactById(id);

        return contact;
    }
}