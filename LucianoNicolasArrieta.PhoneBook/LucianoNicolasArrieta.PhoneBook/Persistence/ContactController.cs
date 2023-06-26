using LucianoNicolasArrieta.PhoneBook.Models;
using Spectre.Console;

namespace LucianoNicolasArrieta.PhoneBook.Persistence;

public class ContactController
{
    ContactContext db = new ContactContext();
    TableVisualization tableVisualization = new TableVisualization();
    UserInput userInput = new UserInput();

    public void Delete()
    {
        List<Contact> contacts = GetContacts();

        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.ContactID);
        }
        int id = userInput.ValidIdInput(existingIds, "contact", "delete");

        var contact = db.Contacts.FirstOrDefault(x => x.ContactID == id);
        db.Remove(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact deleted from the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void InsertInCategory(Category category)
    {
        Contact contact = userInput.ContactInput();
        contact.CategoryID = category.CategoryID;

        db.Add(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact added to the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void Update()
    {
        List<Contact> contacts = GetContacts();

        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.ContactID);
        }
        int id = userInput.ValidIdInput(existingIds, "contact", "update");

        var contact = db.Contacts.FirstOrDefault(x => x.ContactID == id);

        Contact updatedContact = userInput.ContactInput();
        contact.Name = updatedContact.Name;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.Email = updatedContact.Email;

        db.Update(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact updated succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void ViewAll()
    {
        tableVisualization.PrintContacts(GetContacts());

        AnsiConsole.WriteLine("Press any key to return back to the menu.");
        Console.ReadKey();
    }

    public List<Contact> GetContacts()
    {
        List<Contact> contacts = db.Contacts.ToList();

        return contacts;
    }

    public string GetContactEmailById()
    {
        List<Contact> contacts = GetContacts();

        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.ContactID);
        }
        int id = userInput.ValidIdInput(existingIds, "contact", "mail");

        var contact = db.Contacts.FirstOrDefault(x => x.ContactID == id);

        return contact.Email;
    }

    public void ViewContactsByCategory(Category category)
    {
        AnsiConsole.MarkupLine($"[aqua]Contacts in '{category.Name}'[/]");
        tableVisualization.PrintContacts(GetContactsByCategory(category));
    }

    public List<Contact> GetContactsByCategory(Category category)
    {
        List<Contact> contacts = db.Contacts.ToList().FindAll(x => x.CategoryID == category.CategoryID);

        return contacts;
    }
}
