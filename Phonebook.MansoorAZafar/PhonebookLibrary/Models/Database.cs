using PhonebookLibrary.Controllers;
namespace PhonebookLibrary.Models;

internal static class Database
{
    internal static void AddContact()
    {
        Contact person = Utility.CreateContact();
        using var db = new PhonebookContext();
        db.Add(person);
        
        db.SaveChanges();
        System.Console.WriteLine("Successfully Added...");
    }

    internal static List<Contact> GetContact()
    {
        using var db = new PhonebookContext();
        var contacts = db.Contacts;
        return contacts.ToList();
    }

    internal static void DeleteContact(int id)
    {
        using var db = new PhonebookContext();
        db.Remove(new Contact { Id = id });
        
        db.SaveChanges();
        System.Console.WriteLine("Successfully Deleted...");
    }

    internal static void UpdateContact(Contact updatedContact)
    {
        using var db = new PhonebookContext();
        db.Update(updatedContact);

        db.SaveChanges();
        System.Console.WriteLine("Successfully Updated...");
    }

    internal static bool IDExists(int id)
    {
        using var db = new PhonebookContext();
        return db.Contacts.Find(id) != null;
    }
}
