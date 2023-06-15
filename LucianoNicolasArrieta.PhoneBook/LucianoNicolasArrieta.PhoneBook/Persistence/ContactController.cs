using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;

namespace LucianoNicolasArrieta.PhoneBook.Persistence;

public class ContactController
{
    ContactContext db = new ContactContext();
    public void Delete()
    {
        List<Contact> contacts = db.Contacts.ToList();
        TableVisualization tableVisualization = new TableVisualization();
        tableVisualization.printContacts(contacts);

        int id = AnsiConsole.Ask<int>("Enter the id of the contact you want to delete: ");
        var contact = db.Contacts.FirstOrDefault(x => x.Id == id);
        db.Remove(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact deleted from the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void Insert()
    {
        Contact contact = new Contact();
        var name = AnsiConsole.Ask<string>("What's the contact's [aqua]Name[/]?");
        var number = AnsiConsole.Ask<string>("Insert the [aqua]Phone Number[/]: ");
        var email = AnsiConsole.Ask<string>("Insert the [aqua]Email[/]:");

        contact.Name = name;
        contact.PhoneNumber = number;
        contact.Email = email;

        db.Add(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact added to the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void Update()
    {
        List<Contact> contacts = db.Contacts.ToList();
        TableVisualization tableVisualization = new TableVisualization();
        tableVisualization.printContacts(contacts);

        int id = AnsiConsole.Ask<int>("Enter the id of the contact you want to update: ");
        var contact = db.Contacts.FirstOrDefault(x => x.Id == id);

        if (contact != null)
        {
            var name = AnsiConsole.Ask<string>("What's the new [aqua]Name[/]?");
            var number = AnsiConsole.Ask<string>("Insert the new [aqua]Phone Number[/]: ");
            var email = AnsiConsole.Ask<string>("Insert the new [aqua]Email[/]:");

            contact.Name = name;
            contact.PhoneNumber = number;

            db.Update(contact);
            db.SaveChanges();

            AnsiConsole.Markup("[green]Contact updated succesfully![/] Prees any key to continue.");
            Console.ReadKey();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]The id doesn't exist. Try again[/]");
            Console.ReadKey();
            Console.Clear();
            Update();
        }
    }

    public void ViewAll()
    {
        List<Contact> contacts = db.Contacts.ToList();
        TableVisualization tableVisualization = new TableVisualization();
        tableVisualization.printContacts(contacts);

        AnsiConsole.WriteLine("Press any key to return back to the menu.");
        Console.ReadKey();
    }
}
