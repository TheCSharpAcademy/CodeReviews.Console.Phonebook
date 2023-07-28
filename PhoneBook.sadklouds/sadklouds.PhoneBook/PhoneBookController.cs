using sadklouds.PhoneBook.DataAccess;
using sadklouds.PhoneBook.Models;

namespace sadklouds.PhoneBook;

public class PhoneBookController
{
    private ContactContext db = new ContactContext();

    public void CreateContact()
    {
        string name = UserInput.GetContactName("Enter contact name: ");
        string phoneNumber = UserInput.GetContactNumber();
        string email = UserInput.GetContactEmail();
        var c = new Contact
        {
            Name = name,
            PhoneNumber = phoneNumber,
            Email = email
        };
        using (var db = new ContactContext())
        {
            db.Contacts.Add(c);
            db.SaveChanges();
        }
    }

    public void DeleteContact()
    {
        string name = UserInput.GetContactName("Enter contact name: ");

        using (var db = new ContactContext())
        {
            var contact = db.Contacts.Where(c => c.Name == name).FirstOrDefault();
            var records = db.Contacts.ToList();
            if (contact != null)
            {
                db.Remove(contact);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"\nContact {name} could not be found");
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            }
        }
    }

    public Contact? GetContact(string name)
    {
        using (var db = new ContactContext())
        {
            var contact = db.Contacts.Where(c => c.Name == name).FirstOrDefault();
            return contact;
        }
    }

    public void ReadAll()
    {
        using (var db = new ContactContext())
        {
            var records = db.Contacts.ToList();
            foreach (var c in records)
            {
                Console.WriteLine($"Contact: {c.Name}");
                Console.WriteLine("_____________");
            }
        }
    }

    public void ReadContact(string name)
    {
        var contact = GetContact(name);
        if (contact != null)
        {
            Console.WriteLine($"\n_____{contact.Name}_____");
            Console.WriteLine($"Number: {contact.PhoneNumber}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine("__________________");
            Console.Write( "Press any key to continue: ");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"\nContact {name} could not be found");
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }
    }

    public void UpdateContact()
    {
        string currentName = UserInput.GetContactName("Enter contact name: ");
        ReadContact(currentName);

        using (var db = new ContactContext())
        {
            var contact = db.Contacts.Where(c => c.Name == currentName).FirstOrDefault();
            if (contact != null)
            {
                Console.WriteLine("\nUpdate Name(n), Phone number(p), Email(e), All(a) or any other key go back: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "n")
                {
                    contact.Name = UserInput.GetContactName("Enter new contact name: ");
                    db.SaveChanges();
                }
                else if (input.ToLower() == "p")
                {
                    contact.PhoneNumber = UserInput.GetContactNumber();
                    db.SaveChanges();
                }
                else if (input.ToLower() == "e")
                {
                    contact.Email = UserInput.GetContactEmail();
                    db.SaveChanges();
                }
                else if (input.ToLower() == "a")
                {
                    contact.Name = UserInput.GetContactName("Enter new contact name: ");
                    contact.PhoneNumber = UserInput.GetContactNumber();
                    contact.Email = UserInput.GetContactEmail();
                    db.SaveChanges();
                }
                else return;
            }
            else
            {
                Console.WriteLine("\nContact name does not exist\n");
            }
        }
    }
}
