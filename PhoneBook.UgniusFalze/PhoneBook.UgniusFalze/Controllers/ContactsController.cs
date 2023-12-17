using PhoneBook.UgniusFalze.Models;
using PhoneBook.UgniusFalze.Utils;

namespace PhoneBook.UgniusFalze.Controllers;

public static class ContactsController
{
    public static List<Contact> GetContacts()
    {
        using var db = new PhonebookContext();
        var contacts = db.Contacts.OrderBy(c => c.ContactId).ToList();
        return contacts;
    }

    public static void AddContact(string name, string email, string phoneNumber)
    {
        using var db = new PhonebookContext();
        db.Contacts.Add(new Contact { Name = name, Email = email, Number = phoneNumber });
        db.SaveChanges();
    }

    public static void Update(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Contacts.Update(contact);
        db.SaveChanges();
    }

    public static void Delete(Contact contact)
    {
        using var db = new PhonebookContext();
        db.Remove(contact);
        db.SaveChanges();
    }

}