using PhoneBook.StevieTV.Models;
using Spectre.Console;

namespace PhoneBook.StevieTV.Database;

public class PhoneBookController
{

    public static List<Contact> GetContacts()
    {
        using var phoneBookContext = new PhoneBookContext();
        return phoneBookContext.Contacts.OrderBy(c => c.Name).ToList();
    }

    public static void AddContact(Contact contact)
    {
        if (AnsiConsole.Confirm($"Add {contact.Name} - {contact.Email} - {contact.Phone}?"))       
        {
            using var phoneBookContext = new PhoneBookContext();
            phoneBookContext.Contacts.Add(contact);
            phoneBookContext.SaveChanges();
        }
    }

    public static void DeleteContact(Contact contact)
    {
        if (AnsiConsole.Confirm($"Remove {contact.Name} - {contact.Email} - {contact.Phone}?"))       
        {
            using var phoneBookContext = new PhoneBookContext();
            phoneBookContext.Contacts.Remove(contact);
            phoneBookContext.SaveChanges();
        }
    }
}