using PhoneBook.Cactus.DataModel;
using PhoneBook.Cactus.DB;

namespace PhoneBook.Cactus.Controller;
public class ContactController
{
    public static void AddContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Add(contact);
        db.SaveChanges();
    }

    public static void DeleteContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Remove(contact);
        db.SaveChanges();
    }

    public static Contact? GetContactById(int id)
    {
        using var db = new ContactContext();
        return db.Contacts.SingleOrDefault(c => c.Id == id);
    }

    public static List<Contact> GetContacts()
    {
        using var db = new ContactContext();
        return db.Contacts.ToList();
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new ContactContext();
        db.Update(contact);
        db.SaveChanges();
    }
}

