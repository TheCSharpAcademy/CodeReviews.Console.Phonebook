using Microsoft.EntityFrameworkCore;
using Phonebook.StanimalTheMan.Models;

namespace Phonebook.StanimalTheMan.Controllers;

internal class ContactController
{
    internal static void AddContact(Contact contact)
    {
        using var db = new ContactsContext();

        db.Add(contact);

        db.SaveChanges();
    }

    internal static void DeleteContact(Contact contact)
    {
        using var db = new ContactsContext();

        db.Remove(contact);

        db.SaveChanges();
    }

    internal static Contact GetContactById(int id)
    {
        using var db = new ContactsContext();
        var contact = db.Contacts
            .SingleOrDefault(contact => contact.Id == id);

        return contact;
    }

    internal static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();

        var contacts = db.Contacts
            .ToList();

        return contacts;
    }
}
