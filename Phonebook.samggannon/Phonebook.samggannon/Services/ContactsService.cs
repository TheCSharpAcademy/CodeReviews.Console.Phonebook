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

    internal static void ViewContact()
    {
        
    }

    internal static void ViewAllContacts()
    {
        List<Contact> contacts = ContactsController.GetAllContacts();
        List<ContactDto> showContacts = new List<ContactDto>();

        foreach(var contact in contacts)
        {
            ContactDto contactDto = new ContactDto
            {
                Name = contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber
            };

            showContacts.Add(contactDto);
        }
        
        UserInterface.ShowContactsTable(showContacts);
    }

    internal static void DeleteContact()
    {
        throw new NotImplementedException();
    }

    internal static void UpdateContact()
    {
        throw new NotImplementedException();
    }
}
