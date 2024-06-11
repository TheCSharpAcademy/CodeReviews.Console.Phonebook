using Microsoft.EntityFrameworkCore;
using PhoneBook.kalsson.DataAccess;
using PhoneBook.kalsson.Models;
using Spectre.Console;

namespace PhoneBook.kalsson.Controllers;

public class ContactController
{
    internal static List<Contact> GetAllContacts()
    {
        using var db = new ContactContext();

        var allContacts = db.Contacts
            .Include(c => c.EmailAddresses)
            .Include(c => c.PhoneNumbers)
            .ToList();

        return allContacts;
    }

    internal static void AddContact()
    {
        try
        {
            var firstName = AnsiConsole.Ask<string>("Firstname: ");
            var lastName = AnsiConsole.Ask<string>("Lastname: ");
            var emailAddress = AnsiConsole.Ask<string>("Email: ");
            var phoneNumber = AnsiConsole.Ask<string>("Phone: ");
            
            using var db = new ContactContext();

            var contactEmail = new Email { EmailAddress = emailAddress };
            var contactPhone = new Phone { PhoneNumber = phoneNumber };

            var emailList = new List<Email> { contactEmail };
            var phoneList = new List<Phone> { contactPhone };

            db.Add(new Contact
                { FirstName = firstName, LastName = lastName, EmailAddresses = emailList, PhoneNumbers = phoneList });

            db.SaveChanges();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have added a new contact.");
            Console.ResetColor();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while adding the contact. Please try again.");
            Console.WriteLine($"Error detail: {e.Message}");
            throw;
        }
    }

    internal static Contact GetContactById(int id)
    {
        using var db = new ContactContext();

        var contact = db.Contacts.FirstOrDefault(c => c.Id == id);

        return contact;
    }

    internal static void UpdateContact()
    {
        throw new NotImplementedException();
    }

    internal static void DeleteContact()
    {
        throw new NotImplementedException();
    }
}