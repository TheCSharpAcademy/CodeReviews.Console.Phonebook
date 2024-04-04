using Microsoft.EntityFrameworkCore;
using phonebook.Fennikko.Models;

namespace phonebook.Fennikko.Controllers;

public class ContactInfoController
{
    public static void AddContact(ContactInfo contactInfo)
    {
        using var db = new ContactContext();
        db.Add(contactInfo);
        db.SaveChanges();
    }

    public static void DeleteContact(ContactInfo contactInfo)
    {
        using var db = new ContactContext();
        db.Remove(contactInfo);
        db.SaveChanges();
    }

    public static void UpdateContact(ContactInfo contactInfo)
    {
        using var db = new ContactContext();
        db.Update(contactInfo);
        db.SaveChanges();
    }

    public static ContactInfo GetContactById(int id)
    {
        using var db = new ContactContext();
        var contact = db.Contacts
            .Include(c => c.Category)
            .SingleOrDefault(c => c.ContactId == id);

        return contact;
    }
    public static List<ContactInfo> GetContacts()
    {
        using var db = new ContactContext();

        var contacts = db.Contacts
            .Include(c => c.Category)
            .ToList();

        return contacts;
    }
}