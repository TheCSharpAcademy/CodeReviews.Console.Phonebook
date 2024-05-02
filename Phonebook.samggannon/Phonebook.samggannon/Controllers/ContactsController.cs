using Phonebook.samggannon.Models;

namespace Phonebook.samggannon.Controllers;

internal class ContactsController
{
    internal static void AddContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    internal static List<Contact> GetAllContacts()
    {
        using var db = new ContactContext();
        return db.Contacts.ToList();
    }
}
