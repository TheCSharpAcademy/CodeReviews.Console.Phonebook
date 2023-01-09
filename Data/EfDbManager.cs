using PhoneBookConsole.DbContexts;
using PhoneBookConsole.Models;

namespace PhoneBookConsole.Data;

public class EfDbManager : IDbManager
{
    public void AddNewContact(Contact newContact)
    {
        using (var db = new ContactContext())
        {
            db.Add(newContact);
            db.SaveChanges();
        }
    }

    public void UpdateContact(Contact oldContact, Contact newContact)
    {
        using (var db = new ContactContext())
        {
            var old = db.Contacts.First(x => x.Name == oldContact.Name);
            old.Name = newContact.Name;
            old.PhoneNumber = newContact.PhoneNumber;
            old.Email = newContact.Email;
            db.SaveChanges();
        }
    }

    public void DeleteContact(Contact contact)
    {
        using (var db = new ContactContext())
        {
            var toBeDeleted = db.Contacts.First(x => x.Name == contact.Name);
            db.Remove(toBeDeleted);
            db.SaveChanges();
        }
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contactList;
        using (var db = new ContactContext())
        {
            contactList = db.Contacts.ToList();
        }
        return contactList;
    }
}