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

    internal static void DeleteContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Remove(contact); 
        db.SaveChanges();
    }

    internal static List<Contact> GetAllContacts()
    {
        using var db = new ContactContext();
        return db.Contacts.ToList();
    }

    internal static Contact GetContactById(int contactId)
    {
        using var db = new ContactContext();
        return (db.Contacts.FirstOrDefault(x => x.ContactId == contactId));
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Update(contact);
        db.SaveChanges();
    }
}
