using Phonebook.MartinL_no.Models;

namespace Phonebook.MartinL_no.Controllers;

internal class ContactController
{
    public static void AddContact(string name, string phoneNumber)
    {
        using var db = new ContactsContext();
        db.Add(new Contact { Name = name, PhoneNumber = phoneNumber });

        db.SaveChanges();
    }

    public static void DeleteContact(int id)
    {
        var contact = GetContactById(id);

        if (contact != null)
        {
            using var db = new ContactsContext();
            db.Remove(contact);

            db.SaveChanges();
        }
    }

    public static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();

        var contacts = db.Contacts.ToList();

        return contacts;
    }

    public static Contact GetContactById(int id)
    {
        using var db = new ContactsContext();

        var contact = db.Contacts.SingleOrDefault(c => c.Id == id);

        return contact;
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new ContactsContext();

        db.Update(contact);

        db.SaveChanges();
    }
}
