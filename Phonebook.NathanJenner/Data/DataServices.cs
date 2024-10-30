using Console.Phonebook.App.Entities;

namespace Console.Phonebook.App.Data;

public class DataServices
{
    public ApplicationDbContext appDbContext = new();

    public static void CreateDatabaseIfNotExists()
    {

    }

    public void PostContact(Contact contact)
    {
        appDbContext.Contacts.Add(contact);
        appDbContext.SaveChanges();
    }

    public List<Contact> GetAllContacts()
    {
        List<Contact> contacts = appDbContext.Contacts.ToList();
        return contacts;
    }

    public void DeleteSelectedContact(Contact contact)
    {
        appDbContext.Remove(contact);
        appDbContext.SaveChanges();
    }
}