using Phonebook.Models;

namespace Phonebook.Repositories;
internal class ContactRepository
{
    internal static void AddContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Add(contact);

        db.SaveChanges();
    }

    internal static void DeleteContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Remove(contact);

        db.SaveChanges();
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Update(contact);

        db.SaveChanges();
    }

    internal static List<Contact> GetContacts()
    {
        using var db = new ContactContext();
        var contacts = db.Contacts.ToList();

        if (!contacts.Any())
        {
            return Enumerable.Empty<Contact>().ToList();
        }

        return contacts;
    }

    internal static Contact? GetContactById(int id)
    {
        using var db = new ContactContext();
        var contact = db.Contacts.SingleOrDefault(x => x.Id == id);

        return contact;
    }
}
