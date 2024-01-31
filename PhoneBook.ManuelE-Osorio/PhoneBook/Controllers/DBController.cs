using Microsoft.EntityFrameworkCore;

namespace PhoneBookProgram;

public class DBController: IDisposable
{
    private PhoneBookContext db;

    public DBController()
    {
        db = new();
    }

    public void DBInit()
    {
        try
        {
            db.Database.OpenConnection();
            db.Database.CanConnect();
        }
        catch
        {
            throw new Exception("The app cannot connect to the Database. "+
                "Please check your Connection String configuration in your appsettings.json");
        }
        db.Database.EnsureCreated();
    }

    public int GetContactID(string input)
    {
        var contactId = db.Contacts.Where(p => p.ContactName == input).First().ContactId;
        return contactId;
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = [.. db.Contacts.OrderBy(p => p.ContactName)];
        return contacts;
    }

    public List<Contact> GetContacts(char value)
    {
        List<Contact> contacts = [];
        contacts = db.Contacts.OrderBy(p => p.ContactName).ToList()
            .Where(p => p.ContactName.StartsWith(value) || 
            p.ContactName.StartsWith(char.ToUpper(value)) ).ToList();
        return contacts;
    }

    public List<Contact> GetContacts(string value)
    {
        List<Contact> contacts = [];
        contacts = db.Contacts.OrderBy(p => p.ContactName).ToList()
            .Where(p => p.Category != null && 
            p.Category.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
        return contacts;
    }

    public List<Email> GetEmails(int contactId)
    {
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

    public void Insert(string newContactName, string newCategoryName)
    {
        db.Contacts.Add(new Contact{ContactName = newContactName, Category = newCategoryName});
        db.SaveChanges();
    }

    public void Insert(Email objectToInsert)
    {
        Contact? contact = new();
        try
        {
            contact = db.Contacts
                .Include(p => p.Emails)
                .Where(p => p.ContactId == objectToInsert.ContactId).First();
        }
        catch
        {
            throw new Exception("Contact doesn't exists");   
        }

        if(!contact.Emails.Exists(p => p.LocalName == objectToInsert.LocalName 
            && p.DomainName == objectToInsert.DomainName))
        {
            contact.Emails.Add(objectToInsert);
            db.SaveChanges();
        }
    }

    public void Insert(PhoneNumber objectToInsert)
    {
        Contact? contact = new();
        try
        {
            contact = db.Contacts.Include(p => p.PhoneNumbers)
                .Where(p => p.ContactId == objectToInsert.ContactId).First();
        }
        catch
        {
            throw new Exception("Contact doesn't exist");
        }

        if(!contact.PhoneNumbers.Exists(p => p.LocalNumber == objectToInsert.LocalNumber 
            && p.CountryCode == objectToInsert.CountryCode))
        {
            contact.PhoneNumbers.Add(objectToInsert);
            db.SaveChanges();
        }
    }

    public void Modify(Contact modifyContact)
    {
        var contactToModify = db.Contacts.Where( p => p.ContactId == modifyContact.ContactId).First();
        contactToModify.ContactName = modifyContact.ContactName;
        contactToModify.Category = modifyContact.Category;
        db.SaveChanges();
    }

    public void Modify(Email objectToModify)
    {
        var contactToModify = db.Contacts.Include(p => p.Emails)
            .Where( p => p.ContactId == objectToModify.ContactId).First();

        var emailToModify = contactToModify.Emails
            .Find(p => p.EmailId == objectToModify.EmailId);
        
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

    public void Delete(int contactId)
    {
        var contactToRemove = db.Contacts.Where(p => p.ContactId == contactId).First();
        db.Contacts.Remove(contactToRemove);
        db.SaveChanges();
    }

    public void Delete(Email objectToDelete)
    {
        var contact = db.Contacts.Include(p => p.Emails)
            .Where(p => p.ContactId == objectToDelete.ContactId).First();
        contact.Emails.Remove(objectToDelete);
        db.SaveChanges();
    }

    public void Delete(PhoneNumber objectToDelete)
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