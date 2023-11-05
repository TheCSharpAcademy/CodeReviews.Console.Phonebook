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
        var categoryId = GetCategoryInput().CategoryId;

        using var db = new ContactsContext();
        db.Add(new Contact
        {
            Name = name,
            EmailAdress = emailAdress,
            PhoneNumber = phoneNumber,
            CategoryId = categoryId
         });
        db.SaveChanges();
    }

    private Category GetCategoryInput()
    {
        using var db = new ContactsContext();
        var category = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .Title("Choose a Category:")
                                            .AddChoices(db.Categories.Select(c => c.Name).ToList()));
        return db.Categories.Single(x => x.Name == category);
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

    internal IEnumerable<Contact> GetAllContacts()
    {
        using var db = new ContactsContext();
        return db.Contacts;        
    }

    internal Contact GetContact()
    {
        return GetContactInput();
    } 

    private Contact GetContactInput(string message = "")
    {
        using var db = new ContactsContext();

        var contact = AnsiConsole.Prompt(new SelectionPrompt<string>()
                                            .AddChoices(db.Contacts.Select(x => x.Name).ToArray())
                                            .Title(message));
        return db.Contacts.Single(x => x.Name == contact);      
    }
}
