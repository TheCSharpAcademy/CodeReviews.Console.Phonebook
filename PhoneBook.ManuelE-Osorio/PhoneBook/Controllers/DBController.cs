using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace PhoneBookProgram;

public class DBController: IDisposable
{
    private PhoneBookContext db;

    public DBController()
    {
        db = new();
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = [.. db.Contacts];
        return contacts;
    }

    public List<Email> GetEmails(int contactId)
    {
        // List<Email> emails = [];
        var emails = db.Contacts.Include(p => p.Emails)
            .Where(p => p.ContactId == contactId).First().Emails;
        return emails;
    }

    public List<PhoneNumber> GetPhones(int contactId)
    {
        var phones = db.Contacts.Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == contactId).First().PhoneNumbers;
        return phones;
    }

    public void DeleteContact(int contactId)
    {
        var contactToRemove = db.Contacts.Where(p => p.ContactId == contactId).First();
        db.Contacts.Remove(contactToRemove);
        db.SaveChanges();
    }

    public void InsertContact(string newContactName)
    {
        db.Contacts.Add(new Contact{ContactName = newContactName});
        db.SaveChanges();
    }

    public void ModifyContact(string modifyContactName, int contactId)
    {
        var contactToModify = db.Contacts.Where( p => p.ContactId == contactId).First();
        contactToModify.ContactName = modifyContactName;
        db.SaveChanges();
    }

    public void DeleteEmail(Email objectToDelete)
    {
        var contact = db.Contacts.Include(p => p.Emails)
            .Where(p => p.ContactId == objectToDelete.ContactId).First();
        
        contact.Emails.Remove(objectToDelete);
        db.SaveChanges();
    }

    public void DeletePhoneNumber(PhoneNumber objectToDelete)
    {
        var contact = db.Contacts.Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == objectToDelete.ContactId).First();
        
        contact.PhoneNumbers.Remove(objectToDelete);
        db.SaveChanges();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}