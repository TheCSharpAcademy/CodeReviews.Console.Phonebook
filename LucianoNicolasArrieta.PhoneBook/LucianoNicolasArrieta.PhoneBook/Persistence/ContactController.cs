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
        tableVisualization.printContacts(contacts);
        
        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.Id);
        }
        int id = userInput.ValidIdInput(existingIds, "delete");
        
        var contact = db.Contacts.FirstOrDefault(x => x.Id == id);
        db.Remove(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact deleted from the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void Insert()
    {
        Contact contact = userInput.ContactInput();

        db.Add(contact);
        db.SaveChanges();

        AnsiConsole.Markup("[green]Contact added to the Phone Book succesfully![/] Prees any key to continue.");
        Console.ReadKey();
    }

    public void Update()
    {
        List<Contact> contacts = db.Contacts.ToList();
        tableVisualization.printContacts(contacts);

        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.Id);
        }
        int id = userInput.ValidIdInput(existingIds, "update");

        var contact = db.Contacts.FirstOrDefault(x => x.Id == id);

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
        tableVisualization.printContacts(GetContacts());

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
        tableVisualization.printContacts(contacts);

        List<int> existingIds = new List<int>();
        foreach (Contact aux_contact in contacts)
        {
            existingIds.Add(aux_contact.Id);
        }
        int id = userInput.ValidIdInput(existingIds, "mail");

        var contact = db.Contacts.FirstOrDefault(x => x.Id == id);

        return contact.Email;
    }
}
