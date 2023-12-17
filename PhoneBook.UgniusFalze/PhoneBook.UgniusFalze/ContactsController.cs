namespace PhoneBook.UgniusFalze;

public class ContactsController
{
    public static List<Contact> GetContacts()
    {
        using var db = new ContactsContext();
        var contacts = db.Contacts.OrderBy(c => c.ContactId).ToList();
        return contacts;
    }

    public static void AddContact(string name, string email, string phoneNumber)
    {
        using var db = new ContactsContext();
        db.Contacts.Add(new Contact { Name = name, Email = email, Number = phoneNumber });
        db.SaveChanges();
    }
}