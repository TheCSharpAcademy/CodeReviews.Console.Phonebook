namespace PhoneBook;

public class DataAccess
{
    public static void AddContact(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Add(contact);
        context.SaveChanges();
    }

    public static IEnumerable<Contact> GetContacts()
    {
        using var context = new PhoneBookContext();
        return [.. context.Contacts];
    }

    public static void UpdateContact(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Contacts.Update(contact);
        context.SaveChanges();
    }

    public static void RemoveContact(Contact contact)
    {
        using var context = new PhoneBookContext();
        context.Contacts.Remove(contact);
        context.SaveChanges();
    }
}