using Microsoft.EntityFrameworkCore;
using Phonebook.Model;

namespace Phonebook.Controllers;

internal class PhonebookController
{
    internal static void AddContact(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Add(contact);
        db.SaveChanges();
    }
    internal static List<Contact> GetContacts()
    {
        using var db = new PhonebookContext();
        var contacts = db.Contacts
            .Include(x => x.Category)
            .ToList();
        return contacts;
    }
    internal static Contact GetContactById(int id)
    {
        using var db = new PhonebookContext();
        var contact = db.Contacts
            .Include(x => x.Category)
            .SingleOrDefault(x => x.ContactId == id);
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
