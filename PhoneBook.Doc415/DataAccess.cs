using PhoneBook.Doc415.Context;
using PhoneBook.Doc415.Models;

namespace PhoneBook.Doc415;

internal class DataAccess
{
    public void AddContact(string _name, string _email, string _phone, string _title)
    {
        Contact newContact= new Contact();
        newContact.Name= _name;
        newContact.Email= _email;
        newContact.Title= _title;
        newContact.PhoneNumber= _phone;

        try
        {
            var db = new PhoneBookContext();
            db.Contacts.Add(newContact);
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error saving contact: {ex.Message}");
        }
    }

    public List<Contact> GetContacts()
    {
        using var db = new PhoneBookContext();
        return db.Contacts.OrderBy(x=>x.Name).ToList();
    }

    public void DeleteContact(Contact todelete)
    {
        using var db = new PhoneBookContext();
        db.Contacts.Remove(todelete);
        db.SaveChanges();
    }

    public void UpdateContact(string _name, string _title, string _phone,string _email,Contact toUpdate)
    {
        toUpdate.Name= _name;
        toUpdate.Email= _email;
        toUpdate.Title= _title;
        toUpdate.PhoneNumber= _phone;

        var db = new PhoneBookContext();
        db.Update(toUpdate);
        db.SaveChanges();
    }    
}
