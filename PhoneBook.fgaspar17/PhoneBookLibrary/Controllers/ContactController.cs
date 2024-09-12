namespace PhoneBookLibrary;

public static class ContactController
{
    public static List<Contact> GetContacts()
    {
        using var db = new PhoneBookContext();
        var contacts = db.Contacts.ToList();
        return contacts;
    }

    public static Contact? GetContactByName(string name)
    {
        using var db = new PhoneBookContext();
        var contact = db.Contacts.SingleOrDefault(c => c.Name == name);
        return contact;
    }

    public static void InsertContact(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Contacts.Update(contact);
        db.SaveChanges();
    }

    public static void DeleteContact(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Contacts.Remove(contact);
        db.SaveChanges();
    }
}