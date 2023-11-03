using Phonebook.K_MYR.Models;
using Spectre.Console;

namespace Phonebook.K_MYR;

internal class ContactsService
{
    internal void AddContact()
    {
        var name = AnsiConsole.Ask<string>("Contact Name:");
        var phoneNumber = AnsiConsole.Ask<string>("Phone Number:");
        var emailAdress = AnsiConsole.Ask<string>("Email Adress:");

        using var db = new ContactsContext();
        db.Add(new Contact(name, emailAdress, phoneNumber ));
        db.SaveChanges();
    }

    internal void DeleteContact()
    {
        using var db = new ContactsContext();
        var contact = GetContactInput("Which Contact Do You Want To Delete?");
        db.Remove(contact);
        db.SaveChanges();
    }

    internal void UpdateContact()
    {
        using var db = new ContactsContext();
        var contact = GetContactInput("Which Contact Do You Want To Update?");
        
        if (AnsiConsole.Confirm("Update Name?"))
            contact.Name = AnsiConsole.Ask<string>("Name:");
        
        if (AnsiConsole.Confirm("Update Email-Adress?"))
            contact.EmailAdress = AnsiConsole.Ask<string>("Email Adress:");

        if (AnsiConsole.Confirm("Update Phone Number?"))
            contact.Name = AnsiConsole.Ask<string>("Phone Number:");

        db.Update(contact);
        db.SaveChanges();
    }

    internal List<Contact> GetContacts()
    {
        using var db = new ContactsContext();
        return db.Contacts.ToList();        
    }

    internal Contact GetContactInput(string message = "")
    {
        using var db = new ContactsContext();

        var contact = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .AddChoices(db.Contacts.Select(x => x.Name).ToArray())
                                            .Title(message));
        return db.Contacts.Single(x => x.Name == contact);      
    }
}
