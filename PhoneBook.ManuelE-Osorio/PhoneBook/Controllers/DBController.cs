using System.Runtime.CompilerServices;
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

    public Contact GetEmails(int contactId)
    {
        // List<Email> emails = [];
        var emails = db.Contacts.Include(p => p.Emails)
            .Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == contactId).First();
        return emails;
    }

    public List<PhoneNumber> GetPhones(int contactId)
    {
        var phones = db.Contacts.Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == contactId).First().PhoneNumbers;
        return phones;
    }

    public void Delete(int contactId)
    {
        var contactToRemove = db.Contacts.Where(p => p.ContactId == contactId).First();
        db.Contacts.Remove(contactToRemove);
        db.SaveChanges();
    }

    public void Insert(string newContactName)
    {
        db.Contacts.Add(new Contact{ContactName = newContactName});
        db.SaveChanges();
    }

    public void Modify(string modifyContactName, int contactId)
    {
        var contactToModify = db.Contacts.Where( p => p.ContactId == contactId).First();
        contactToModify.ContactName = modifyContactName;
        db.SaveChanges();
    }

    public void Delete(Email objectToDelete)
    {
        var contact = db.Contacts.Include(p => p.Emails)
            .Where(p => p.ContactId == objectToDelete.ContactId).First();

        var email = contact.Emails.Find(p => p.EmailId == objectToDelete.EmailId);
        if(email != null)
            contact.Emails.Remove(email);

        db.SaveChanges();
    }

    public void Delete(PhoneNumber objectToDelete)
    {
        var contact = db.Contacts.Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == objectToDelete.ContactId).First();
        
        contact.PhoneNumbers.Remove(objectToDelete);
        db.SaveChanges();
    }

    public void Insert(Email objectToInsert)
    {
        var contact = db.Contacts
            .Include(p => p.Emails)
            .Where(p => p.ContactId == objectToInsert.ContactId).First();

        contact.Emails.Add(objectToInsert);
        db.SaveChanges();
    }

    public void Insert(PhoneNumber objectToInsert)
    {
        var contact = db.Contacts.Include(p => p.PhoneNumbers)
            .Where(p => p.ContactId == objectToInsert.ContactId).First();

        contact.PhoneNumbers.Add(objectToInsert);
        db.SaveChanges();
    }

    public void Modify(Email objectToModify)
    {
        var contactToModify = db.Contacts.Include(p => p.Emails)
            .Where( p => p.ContactId == objectToModify.ContactId).First();
        
        var emailToModify = contactToModify.Emails.Find(p => p.EmailId == objectToModify.EmailId);
        emailToModify = objectToModify;
        db.SaveChanges();
    }

    public void Modify(PhoneNumber objectToModify)
    {
        var contactToModify = db.Contacts.Include(p => p.PhoneNumbers)
            .Where( p => p.ContactId == objectToModify.ContactId).First();
        
        var phoneNumberToModify = contactToModify.PhoneNumbers
            .Find(p => p.PhoneNumberId == objectToModify.PhoneNumberId);
        
        phoneNumberToModify = objectToModify;
        db.SaveChanges();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}