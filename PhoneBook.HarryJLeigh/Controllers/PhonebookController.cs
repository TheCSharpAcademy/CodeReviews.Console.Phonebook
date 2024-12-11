using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Utilities;
using Phonebook.Views;
using Spectre.Console;

namespace Phonebook.Controllers;

public class PhonebookController
{
    private readonly PhonebookService _phonebookService = new PhonebookService();

    internal void ViewContacts()
    {
        var contacts = GetAllContacts();
        Console.Clear();
        if (contacts.Count == 0)
            AnsiConsole.MarkupLine("[yellow]No contacts found![/]");
        else
            TableVisualisation.ShowTable(contacts);
    }

    internal void ViewContactsByFilter(string filter)
    {
        var filteredContacts = GetContactsByCategory(filter);
        TableVisualisation.ShowTable(filteredContacts);
        Util.AskUserToContinue();
    }

    internal void CreateContact(Contact contact)
    {
        if (Util.ReturnToMenu()) return;
        _phonebookService.CreateContact(contact);
        AnsiConsole.MarkupLine("[yellow]Success! Created new contact.[/]");
        Util.AskUserToContinue();
    }

    internal void UpdateContact(
        bool updateName = false, 
        bool updateEmail = false, 
        bool updateNumber = false, 
        bool updateCategory = false)
    {
        ViewContacts();
        var contactIds = GetContactsId();
        
        if (contactIds.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }

        int contactId = UserInputHelper.GetId(contactIds, "update");
        var contact = GetContactById(contactId);

        if (updateName == true) contact.Name = UserInputHelper.GetName("update");
        if (updateEmail == true) contact.Email = UserInputHelper.GetEmail("update");
        if (updateNumber == true) contact.PhoneNumber = UserInputHelper.GetPhoneNumber("update");
        if (updateCategory == true) contact.Category = UserInputHelper.GetCategory("update");
        if (Util.ReturnToMenu()) return;
        
        _phonebookService.UpdateContact(contact);
        
        AnsiConsole.MarkupLine("[yellow]Success! Updated contact.[/]");
        Util.AskUserToContinue();
    }

    internal void DeleteContact()
    {
        ViewContacts();
        var contacts = GetAllContacts();
        
        if (contacts.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }

        List<int> contactIds = GetContactsId();
        int contactId = UserInputHelper.GetId(contactIds, "delete");
        if (Util.ReturnToMenu()) return;
        
        _phonebookService.DeleteContact(contactId);
        
        AnsiConsole.MarkupLine("[yellow]Success! Contact deleted.[/]");
        Util.AskUserToContinue();
    }
    
    internal List<int> GetContactsId() => _phonebookService.GetContactsId();
    
    internal List<Contact> GetAllContacts() => _phonebookService.GetAllContacts();
    
    internal Contact GetContactById(int id) => _phonebookService.GetContactById(id);

    internal List<Contact> GetContactsByCategory(string category) => 
        _phonebookService.GetContactsByCategory(category);
}