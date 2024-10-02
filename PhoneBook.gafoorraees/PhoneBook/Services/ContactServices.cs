using PhoneBook.Controller;
using PhoneBook.Models;
using Spectre.Console;

namespace PhoneBook.Services;

internal class ContactServices
{
    internal static void InsertContact()
    {
        var contact = new Contact
        {
            Name = Validation.GetValidName(),
            PhoneNumber = Validation.GetValidPhoneNumber(),
            Email = Validation.GetValidEmail()
        }; 

        ContactController.AddContact(contact);

        Console.WriteLine("\nContact added successfully. Enter any key to return to the main menu.\n");
        Console.ReadKey();
    }

    internal static void DeleteContact()
    {
        var contact = GetContactOptionInput();

        ContactController.DeleteContact(contact);

        Console.WriteLine("\nContact deleted successfully. Enter any key to return to the main menu.\n");
        Console.ReadKey();
    }

    internal static void UpdateContact()
    {
        var contact = GetContactOptionInput();

        contact.Name = AnsiConsole.Confirm("Update name?")
            ? Validation.GetValidName()
            : contact.Name;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?")
            ? Validation.GetValidPhoneNumber()
            : contact.PhoneNumber;
        
        contact.Email = AnsiConsole.Confirm("Update email address?")
            ? Validation.GetValidEmail()
            : contact.Email;

        ContactController.UpdateContact(contact);

        Console.WriteLine("\nContact updated successfuly. Enter any key to return to the main menu.\n");
        Console.ReadKey();
    }

    internal static void GetContacts()
    {
        var contacts = ContactController.GetContacts();
        UserInterface.ShowContactsTable(contacts); 
    }

    static private Contact GetContactOptionInput()
    {
        var contacts = ContactController.GetContacts();
        
        if (!contacts.Any())
        {
            Console.WriteLine("No contacts available.");
            Console.WriteLine("Enter any key to return the main menu");
            Console.ReadLine();
            UserInterface.MainMenu();
        }
        var contactsArray = contacts.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose a Contact")
            .AddChoices(contactsArray));
        var id = contacts.Single(x => x.Name == option).Id;

        return ContactController.GetContactById(id);
    }
}
