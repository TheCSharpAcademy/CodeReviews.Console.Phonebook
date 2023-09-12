using Microsoft.EntityFrameworkCore;

namespace Phonebook;

internal class Controller
{
    internal static void Init()
    {
        using (var context = new Context())
        {
            context.Database.Migrate();
        }
    }
    internal static void AddContact(Contact contact)
    {
        using (var context = new Context())
        {
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
    }
    internal static void DeleteContact(Contact contact)
    {
        using (var context = new Context())
        {
            context.Contacts.Remove(contact);
            context.SaveChanges();
        }
    }
    internal static List<Contact> GetContacts()
    {
        List<Contact> contacts = new();
        using (var context = new Context())
        {
            contacts = context.Contacts.ToList();
        }
        return contacts;
    }
    internal static Contact? GetContactName(string name)
    {
        Contact contact = new();
        using (var context = new Context())
        {
            contact = context.Contacts.SingleOrDefault(x => x.Name == name);
        }
        return contact;
    }
    internal static void UpdateContact(Contact target, Contact update)
    {
        using (var context = new Context())
        {
            Contact existing = context.Contacts.First(x=>x.Name == target.Name);
            update.ContactID = existing.ContactID;
            context.Entry(existing).CurrentValues.SetValues(update);
            context.SaveChanges();
        }
    }internal static void UpdateContactDate(Contact target)
    {
        using (var context = new Context())
        {
            Contact existing = context.Contacts.First(x=>x.Name == target.Name);
            existing.LastAccess = DateTime.Now;
            context.SaveChanges();
        }
    }
}
