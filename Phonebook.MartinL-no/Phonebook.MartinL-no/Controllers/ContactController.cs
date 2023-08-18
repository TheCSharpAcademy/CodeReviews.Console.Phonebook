using Microsoft.EntityFrameworkCore;

using Phonebook.MartinL_no.Models;

namespace Phonebook.MartinL_no.Controllers;

internal class ContactController
{
    public static void AddContact(Contact contact)
    {
        using var db = new ContactsContext();
        db.Add(contact);

        db.SaveChanges();
    }

    public static void DeleteContact(Contact contact)
    {
        using var db = new ContactsContext();
        db.Remove(contact);

        db.SaveChanges();
    }

    public static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();

        var contacts = db.Contacts.Include(x => x.category).ToList();

        return contacts;
    }

    public static Contact GetContactById(int id)
    {
        using var db = new ContactsContext();

        var contact = db.Contacts.Include(x => x.category).FirstOrDefault(c => c.Id == id);

        return contact;
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new ContactsContext();

        db.Update(contact);

        db.SaveChanges();
    }
}
