using PhoneBook.Models;

namespace PhoneBook;

public static class DatabaseInterface
{
    public static List<Contact> GetContacts()
    {
        using var db = new PhoneBookContext();

        List<Contact> contacts = db.Contacts.ToList();

        return contacts;
    }

    public static Contact GetContact(int id)
    {
        using var db = new PhoneBookContext();

        try
        {
            Contact contact = db.Contacts.Where(contact => contact.Id == id).First();
            return contact;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static void CreateContact(string firstName, string lastName, string phoneNumber)
    {
        using var db = new PhoneBookContext();

        db.Add(new Contact { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber });
        db.SaveChanges();
    }

    public static void UpdateFirstName(string firstName, int id)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Where(contact => contact.Id == id).First().FirstName = firstName;
        db.SaveChanges();
    }

    public static void UpdateLastName(string lastName, int id)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Where(contact => contact.Id == id).First().LastName = lastName;
        db.SaveChanges();
    }

    public static void UpdatePhoneNumber(string phoneNumber, int id)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Where(contact => contact.Id == id).First().PhoneNumber = phoneNumber;
        db.SaveChanges();
    }

    public static void DeleteContact(int id)
    {
        using var db = new PhoneBookContext();

        db.Contacts.Remove(GetContact(id));
        db.SaveChanges();
    }

    public static bool ContactExists(int id)
    {
        using var db = new PhoneBookContext();
        bool exists = false;

        if (GetContact(id) != null) exists = true;

        return exists;
    }
}
