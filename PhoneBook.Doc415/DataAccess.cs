using PhoneBook.Doc415.Context;
using PhoneBook.Doc415.Models;

namespace PhoneBook.Doc415;

internal class DataAccess
{
    private readonly PhoneBookContext db = new();
    public void AddContact(string _name, string _email, string _phone, string _title,string _category)
    {
        Contact newContact = new Contact()
        {
            Name = _name,
            Email = _email,
            Title = _title,
            PhoneNumber = _phone,
            Category = _category
        };

        try
        {
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
        return db.Contacts.OrderBy(x=>x.Name).ToList();
    }

    public List<Contact> GetContactsByCategory(string _category)
    {
        return db.Contacts.Where(x => x.Category==_category).ToList();
    }

    public List<string> GetCategories()
    {
        return db.Contacts.Select(x=> x.Category).Distinct().ToList();
    }

    public void DeleteContact(Contact todelete)
    {
        db.Contacts.Remove(todelete);
        db.SaveChanges();
    }

    public void UpdateContact(string _name, string _title, string _phone,string _email,string _category,Contact toUpdate)
    {
        toUpdate.Name= _name;
        toUpdate.Email= _email;
        toUpdate.Title= _title;
        toUpdate.PhoneNumber= _phone;
        toUpdate.Category= _category;

        db.Update(toUpdate);
        db.SaveChanges();
    }    
}
