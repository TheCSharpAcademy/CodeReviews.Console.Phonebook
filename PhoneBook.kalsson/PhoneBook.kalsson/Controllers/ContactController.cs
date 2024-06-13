using Microsoft.EntityFrameworkCore;
using PhoneBook.kalsson.DataAccess;
using PhoneBook.kalsson.Models;
using Spectre.Console;

namespace PhoneBook.kalsson.Controllers;

public class ContactController
{
    /// <summary>
    /// Retrieves all the contacts from the phone book.
    /// </summary>
    /// <returns>A list of Contact objects representing all the contacts in the phone book.</returns>
    internal static List<Contact> GetAllContacts()
    {
        using var db = new ContactContext();

        var allContacts = db.Contacts
            .Include(c => c.EmailAddresses)
            .Include(c => c.PhoneNumbers)
            .ToList();

        return allContacts;
    }

    /// <summary>
    /// Adds a new contact to the phone book.
    /// </summary>
    /// <remarks>
    /// The contact information includes the first name, last name, email address, and phone number.
    /// The user is prompted to enter each piece of information individually, with some basic validation performed
    /// on the email address and phone number.
    /// The contact is then added to the database using the ContactContext class, and the changes are saved.
    /// If an error occurs during the contact addition process, an exception is thrown and error details are displayed.
    /// </remarks>
    internal static void AddContact()
    {
        try
        {
            var firstName = AnsiConsole.Ask<string>("Firstname: ");
            var lastName = AnsiConsole.Ask<string>("Lastname: ");
            
            var emailAddress = AnsiConsole.Ask<string>("Email: ");
            while(!InputValidator.ValidateEmail(emailAddress)) 
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                emailAddress = AnsiConsole.Ask<string>("Email: ");
            }
            
            var phoneNumber = AnsiConsole.Ask<string>("Phone: ");
            while(!InputValidator.ValidatePhoneNumber(phoneNumber)) 
            {
                Console.WriteLine("Invalid phone number. Please enter a 10 digit phone number with no dashes or spaces.");
                phoneNumber = AnsiConsole.Ask<string>("Phone: ");
            }
            
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

    /// <summary>
    /// Updates a contact in the phone book.
    /// </summary>
    /// <remarks>
    /// This method allows the user to select a contact from the existing contacts in the phone book
    /// and update its details including first name, last name, email address, and phone number. The
    /// updated contact is then saved in the database.
    /// </remarks>
    /// <exception cref="System.Exception">Thrown when an error occurs while updating the contact.</exception>
    internal static void UpdateContact()
{
    try
    {
        using var db = new ContactContext();
        var contacts = db.Contacts
            .Include(c => c.EmailAddresses)
            .Include(c => c.PhoneNumbers)
            .ToList();

        var selectionPrompt = new SelectionPrompt<string>()
            .Title("Please select the contact you wish to update:")
            .PageSize(10);

        foreach (var contact in contacts)
        {
            selectionPrompt.AddChoice($"{contact.FirstName} {contact.LastName} (ID: {contact.Id})");
        }

        var selectedContactDetail = AnsiConsole.Prompt(selectionPrompt);
        var id = int.Parse(selectedContactDetail.Split(" (ID: ")[1].Replace(")", ""));
        var contactToUpdate = contacts.First(c => c.Id == id);

        var firstName = AnsiConsole.Ask<string>("Updated Firstname: ");
        var lastName = AnsiConsole.Ask<string>("Updated Lastname: ");

        var emailAddress = AnsiConsole.Ask<string>("Updated Email: ");
        while (!InputValidator.ValidateEmail(emailAddress))
        {
            Console.WriteLine("Invalid email format. Please enter a valid email address.");
            emailAddress = AnsiConsole.Ask<string>("Updated Email: ");
        }

        var phoneNumber = AnsiConsole.Ask<string>("Updated Phone: ");
        while (!InputValidator.ValidatePhoneNumber(phoneNumber))
        {
            Console.WriteLine("Invalid phone number. Please enter a 10 digit phone number with no dashes or spaces.");
            phoneNumber = AnsiConsole.Ask<string>("Updated Phone: ");
        }

        bool isContactUpdated = false;

        if (contactToUpdate.FirstName != firstName)
        {
            contactToUpdate.FirstName = firstName;
            isContactUpdated = true;
        }

        if (contactToUpdate.LastName != lastName)
        {
            contactToUpdate.LastName = lastName;
            isContactUpdated = true;
        }

        if (contactToUpdate.EmailAddresses.First().EmailAddress != emailAddress)
        {
            var email = contactToUpdate.EmailAddresses.First();
            email.EmailAddress = emailAddress;
            db.Entry(email).State = EntityState.Modified;
            isContactUpdated = true;
        }

        if (contactToUpdate.PhoneNumbers.First().PhoneNumber != phoneNumber)
        {
            var phoneNumberRecord = contactToUpdate.PhoneNumbers.First();
            phoneNumberRecord.PhoneNumber = phoneNumber;
            db.Entry(phoneNumberRecord).State = EntityState.Modified;
            isContactUpdated = true;
        }

        if (isContactUpdated)
        {
            db.SaveChanges();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have updated the contact.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("No changes were made to the contact.");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("An error occurred while updating the contact. Please try again.");
        Console.WriteLine($"Error detail: {e.Message}");
        throw;
    }
}

    /// <summary>
    /// Deletes a contact from the phone book.
    /// </summary>
    internal static void DeleteContact()
    {
        try
        {
            using var db = new ContactContext();
            var contacts = db.Contacts
                .Include(c => c.EmailAddresses)
                .Include(c => c.PhoneNumbers)
                .ToList();
            var selectionPrompt = new SelectionPrompt<string>()
                .Title("Please select the contact you wish to delete:")
                .PageSize(10);
            foreach (var contact in contacts)
            {
                selectionPrompt.AddChoice($"{contact.FirstName} {contact.LastName} (ID: {contact.Id})");
            }

            var selectedContactDetail = AnsiConsole.Prompt(selectionPrompt);
            var id = int.Parse(selectedContactDetail.Split(" (ID: ")[1].Replace(")", ""));
            var contactToDelete = contacts.First(c => c.Id == id);

            // Confirm deletion
            Console.Write(
                $"Are you sure you want to delete {contactToDelete.FirstName} {contactToDelete.LastName}? Type yes to confirm, no to cancel: ");

            if (Console.ReadLine().ToLower() == "yes")
            {
                db.Contacts.Remove(contactToDelete);
                db.SaveChanges();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You have deleted the contact.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Deletion was cancelled.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while deleting the contact. Please try again.");
            Console.WriteLine($"Error detail: {e.Message}");
            throw;
        }
    }
}