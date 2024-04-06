using Spectre.Console;

namespace PhoneBook.Cactus;
public class ContactController
{
    public static void AddContact()
    {
        var name = AnsiConsole.Ask<string>("Person's name:");
        var email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        while (!ContactUtil.IsValidEmail(email))
        {
            Console.WriteLine("Please input a valid email.");
            email = AnsiConsole.Ask<string>("Person's email (format xxx@yyy.zzz):");
        }
        var phonNumber = AnsiConsole.Ask<string>("Person's phone number:");

        using var db = new ContactContext();
        db.Add(new Contact { Name = name, Email = email, PhoneNumber = phonNumber });

        db.SaveChanges();
    }

    public static void DeleteContact()
    {
        throw new NotImplementedException();
    }

    public static void GetContactById()
    {
        throw new NotImplementedException();
    }

    public static List<Contact> GetContacts()
    {
        using var db = new ContactContext();
        return db.Contacts.ToList();
    }

    public static void UpdateContact()
    {
        throw new NotImplementedException();
    }
}

