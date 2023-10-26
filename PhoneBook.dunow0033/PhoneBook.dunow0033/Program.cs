using Spectre.Console;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.dunow0033;

internal class Program
{
    private static readonly ContactContext db = new ContactContext();

    internal static void Main()
    {
        bool closeApp = false;

        while (closeApp == false)
        {
            AnsiConsole.Clear();
            Console.WriteLine("Please enter your selection:  ");
            Console.WriteLine("1. List All Contacts");
            Console.WriteLine("3. Add A Contact");
            Console.WriteLine("4. Update A Contact");
            Console.WriteLine("5. Delete A Contact");
            Console.WriteLine("6. Exit\n");
            string selectedOption = Console.ReadLine();

            switch (selectedOption)
            {
                case "1":
                    ListContacts();
                    break;
                case "3":
                    AddContact();
                    break;
                case "4":
                    UpdateContact();
                    break;
                case "5":
                    DeleteContact();
                    break;
                case "6":
                    Console.WriteLine("Thank you!!  Bye!!");
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid option!!  Please try again!!");
                    Thread.Sleep(2000);
                    Main();
                    break;
            }
        }
    }

    private static void ListContacts()
    {
        Console.Clear();
        Console.WriteLine("List of contacts");
        var contacts = db.Contacts.OrderBy(c => c.Id);

        Table table = new Table();
        table.Expand();
        table.AddColumns("Name", "Email", "Phonenumber");

        foreach (var contact in contacts)
        {
            table.AddRow($"{contact.Name}", $"{contact.Email}", $"{contact.PhoneNumber}");
        }
        AnsiConsole.Write(table);

        Console.WriteLine("\nPress any key to return to main menu...");
        Console.ReadKey();
    }

    private static void AddContact()
    {
        Console.Clear();

        string personName = AnsiConsole.Ask<string>("Enter the name of the contact: ");
        string personEmail = AnsiConsole.Ask<string>("Enter the email of the contact: ");

        while (!Validator.ValidateEmail(personEmail))
        {
            personEmail = AnsiConsole.Ask<string>("Invalid email address, try again: ");
        }

        string personPhone = AnsiConsole.Ask<string>("Enter the phone number (numbers and dashes only): ");

        while (!Validator.ValidatePhoneNumber(personPhone))
        {
            personPhone = AnsiConsole.Ask<string>("Invalid phone number, try again: ");
        }

        db.Add(new Contact { Name = personName, Email = personEmail, PhoneNumber = personPhone });

        int saved = db.SaveChanges();

        if (saved > 0)
        {
            Console.WriteLine("Contact has been saved, press any key to return to main menu...");
        }
        else
        {
            Console.WriteLine("There was an error saving your entry, press any key to return to main menu...");
        }

        Console.ReadKey();
    }

    private static void UpdateContact()
    {
        Console.Clear();
        var contacts = db.Contacts.OrderBy(c => c.Name);

        int selectedContact = AnsiConsole.Prompt(DrawContacts(contacts)).Id;

        Contact contact = db.Contacts.Find(selectedContact);

        if (selectedContact == -1)
        {
            Main();
        }

        if (selectedContact != 0)
        {
            string nameUpdate = AnsiConsole.Ask<string>("Do you want to update your name (y/n)?");

            if (nameUpdate.ToLower() == "y")
            {
                contact.Name = AnsiConsole.Ask<string>("Contact's new name: ");
            }

            string emailUpdate = AnsiConsole.Ask<string>("Do you want to update your email address (y/n)?");

            if (emailUpdate.ToLower() == "y")
            {
                contact.Email = AnsiConsole.Ask<string>("Contact's new email: ");
                while (!Validator.ValidateEmail(contact.Email))
                {
                    contact.Email = AnsiConsole.Ask<string>("Invalid email address, try again: ");
                }
            }

            string phoneUpdate = AnsiConsole.Ask<string>("Do you want to update your phone number (y/n)?");

            if (phoneUpdate.ToLower() == "y")
            {
                contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's new phone number  (numbers and dashes only): ");
                while (!Validator.ValidatePhoneNumber(contact.PhoneNumber))
                {
                    contact.PhoneNumber = AnsiConsole.Ask<string>("Invalid phone number, try again: ");
                }
            }

            db.Contacts.Update(contact);

            Console.WriteLine("Contact successfully updated!!  Press any key for the main menu...");
        }
        else
        {
            Console.WriteLine($"There was a problem updating {contact.Name}'s entry.  Press any key for the main menu...");
        }

        Console.ReadKey();
    }

    private static void DeleteContact()
    {
        Console.Clear();
        var contacts = db.Contacts.OrderBy(c => c.Name);

        int selectedContact = AnsiConsole.Prompt(DrawContacts(contacts)).Id;

        if (selectedContact == -1)
        {
            Main();
        }

        Contact contact = db.Contacts.Find(selectedContact);
        string contactName = contact.Name;

        if (selectedContact != 0)
        {
            db.Contacts.Remove(db.Contacts.Find(selectedContact));
            db.SaveChanges();
            Console.WriteLine($"Contact {contactName} has been deleted.  Press any key for the main menu");
        }
        else
        {
            Console.WriteLine($"Failed to delete {contactName}, press any key for the main menu");
        }
        Console.ReadKey();
    }

    private static SelectionPrompt<Menu> DrawContacts(IQueryable<Contact> contacts)
    {
        SelectionPrompt<Menu> menu = new()
        {
            HighlightStyle = new(
                Color.LightSkyBlue1,
                Color.Black,
                Decoration.Underline
            )
        };

        menu.Title("Select a [B]contact to delete[/]");
        foreach (var contact in contacts.ToList())
        {
            menu.AddChoice(new() { Id = contact.Id, Text = contact.Name });
        }

        menu.AddChoice(new() { Id = -1, Text = "**main menu**" });

        return menu;
    }
}
