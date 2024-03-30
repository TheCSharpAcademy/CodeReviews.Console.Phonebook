using Microsoft.EntityFrameworkCore;
using Phonebook.Models;
using Spectre.Console;

namespace Phonebook.Controllers;

internal class ContactController
{
    internal static Contact AddContact(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Add(contact);
        db.SaveChanges();

        return contact; 
    }

    internal static List<Contact> GetContacts()
    {
        using var db = new PhonebookContext();

        var contactsWithEmailsAndPhones = db.Contacts
            .Include(c => c.Emails)
            .Include(c => c.PhoneNumbers)  // Assuming PhoneNumbers is the property for phone numbers in your Contact class
            .ToList();

        return contactsWithEmailsAndPhones;
    }


    internal static Contact GetContactById(int id)
    {
        using var db = new PhonebookContext();
        var contact = db.Contacts.SingleOrDefault(x => x.ContactId == id);
        return contact;

    }

    internal static void DeleteContact(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Remove(contact);
        db.SaveChanges();
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Update(contact);
        db.SaveChanges();
    }

}
