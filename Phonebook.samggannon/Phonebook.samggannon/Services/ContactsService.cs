using Phonebook.samggannon.Controllers;
using Phonebook.samggannon.Models;
using Phonebook.samggannon.Utilities;
using Spectre.Console;

namespace Phonebook.samggannon.Services;

internal class ContactsService
{
    internal static void AddContact()
    {
        var contact = new Contact();
        contact.Name = AnsiConsole.Ask<string>("Enter a name for your contact *Required: ").Trim();
        contact.Email = GetEmailInformation();
        contact.PhoneNumber = AnsiConsole.Ask<string>("Enter a phone number for your contact *Required: ").Trim();

        ContactsController.AddContact(contact);
    }

    private static string GetEmailInformation()
    {
        string email = "";
        bool isValidEmail;
        bool emailIsProvided = AnsiConsole.Confirm("Would you like to enter an email address?");

        if (emailIsProvided)
        {
            email = AnsiConsole.Ask<string>("Contacts Email");
            isValidEmail = EmailValidator.IsEmailValid(email);
            
            while(!isValidEmail)
            {
                AnsiConsole.WriteLine("Invalid email format: ");
                AnsiConsole.WriteLine("email must be formatted like: MyEmailAddress@something[.com, .net, edu, biz, etc...]");

                email = AnsiConsole.Ask<string>("Contacts Email");
                isValidEmail = EmailValidator.IsEmailValid(email);
            }
        }

        return email;
    }

    internal static void ViewAllContacts()
    {
        List<Contact> contacts = ContactsController.GetAllContacts();
        UserInterface.ShowContactsTable(contacts);
    }

    internal static void UpdateContact()
    {
        ViewAllContacts();
        string input = AnsiConsole.Ask<string>("Select the id of the contact you wish to update");

        if (int.TryParse(input, out int contactId))
        {
            Contact contact = GetContactOrPrompt(contactId);
            if (contact != null)
            {
                UpdateContactDetails(contact);
            }
            else
            {
                Console.Clear();
                UpdateContact();
            }
        }
        else
        {
            AnsiConsole.WriteLine("You have entered an invalid input. Please try again. Press a key to continue");
            Console.ReadLine();
            Console.Clear();
            UpdateContact();
        }
    }

    private static void UpdateContactDetails(Contact contact)
    {
        contact.Name = AnsiConsole.Confirm("Update name?")
            ? AnsiConsole.Ask<string>("Contact's new name?")
            : contact.Name;

        contact.Email = AnsiConsole.Confirm("Update Email?")
            ? contact.Email = GetEmailInformation()
            : contact.Email;

        contact.PhoneNumber = AnsiConsole.Confirm("Update phone number?")
            ? AnsiConsole.Ask<string>("Update phone number?")
            : contact.PhoneNumber;

        ContactsController.UpdateContact(contact);
    }

    #region Delete

    internal static void DeleteContact()
    {
        ViewAllContacts();
        string input = AnsiConsole.Ask<string>("Select the id of the contact you wish to delete");

        if (int.TryParse(input, out int contactId))
        {
            Contact contact = GetContactOrPrompt(contactId);
            if (contact != null)
            {
                if (UserConfirmsDeletion(contact))
                {
                    ContactsController.DeleteContact(contact);
                    return;
                }
            }
            else
            {
                Console.Clear();
                DeleteContact();
            }
        }
        else
        {
            AnsiConsole.WriteLine("You have entered an invalid input. Please try again. Press a key to continue");
            Console.ReadLine();
            Console.Clear();
            DeleteContact();
        }
    }

    private static Contact GetContactOrPrompt(int contactId)
    {
        Contact contact = ContactsController.GetContactById(contactId);
        if (contact == null)
        {
            AnsiConsole.WriteLine("Contact not found. Press a key to continue");
            Console.ReadLine();
            Console.Clear();
            return contact;
        }
        return contact;
    }

    private static bool UserConfirmsDeletion(Contact contact)
    {
        UserInterface.ConfirmContact(contact);
        return AnsiConsole.Confirm("Are you sure?");
    }

    #endregion
}
