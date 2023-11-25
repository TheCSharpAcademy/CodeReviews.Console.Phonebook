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

    internal static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();

        var contacts = db.Contacts
            .ToList();

        return contacts;
    }
}
