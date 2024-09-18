using PhoneBook.Models;

namespace PhoneBook.Controller;

internal class ContactController
{
    internal static void AddContact(Contact contact)
    {
        using var db = new PhoneBookContext();

        db.Add(contact);
        db.SaveChanges();
    }

    internal static void DeleteContact(Contact contact)
    {
        using var db = new PhoneBookContext();

        db.Remove(contact);
        db.SaveChanges();
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new PhoneBookContext();

        db.Update(contact);
        db.SaveChanges();
    }

    internal static Contact GetContactById(int id)
    {
        using var db = new PhoneBookContext();

        return db.Contacts.SingleOrDefault(x => x.Id == id);
    }

    internal static List<Contact> GetContacts()
    {
        using var db = new PhoneBookContext();

        var contacts = db.Contacts.ToList();

        return contacts;
    }
}
