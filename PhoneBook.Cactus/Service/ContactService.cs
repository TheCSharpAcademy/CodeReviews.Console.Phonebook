using PhoneBook.Cactus.Controller;
using PhoneBook.Cactus.DataModel;
using PhoneBook.Cactus.UI;
using PhoneBook.Cactus.Util;
using Spectre.Console;

namespace PhoneBook.Cactus.Service;

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
        var phoneNumber = AnsiConsole.Ask<string>("Person's phone number (format 123-456-7899):");
        while (!ContactUtil.IsValidPhoneNumber(phoneNumber))
        {
            Console.WriteLine("Please input a valid phone number.");
            phoneNumber = AnsiConsole.Ask<string>("Person's phone number (format 123-456-7899):");
        }

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

        contact.Name = AnsiConsole.Confirm("Update name?") ? AnsiConsole.Ask<string>("Person's new name:") : contact.Name;
        contact.Email = AnsiConsole.Confirm("Update email?") ? AnsiConsole.Ask<string>("Person's new email (format xxx@yyy.zzz):") : contact.Email;
        while (!ContactUtil.IsValidEmail(contact.Email))
        {
            Console.WriteLine("Please input a valid email.");
            contact.Email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        }
        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?") ? AnsiConsole.Ask<string>("Person's new phone number (format 123-456-7899):") : contact.PhoneNumber;
        while (!ContactUtil.IsValidPhoneNumber(contact.PhoneNumber))
        {
            Console.WriteLine("Please input a valid phone number.");
            contact.PhoneNumber = AnsiConsole.Ask<string>("Person's new phone number (format 123-456-7899):");
        }

        ContactController.UpdateContact(contact);

        UserInterface.BackToMainMenuPrompt();
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
