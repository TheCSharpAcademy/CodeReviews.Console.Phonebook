using Phonebook.Data;
using Phonebook.Models;
using Phonebook.Services;
using Phonebook.Utilities;
using Phonebook.Views;
using Spectre.Console;

namespace Phonebook.Controllers;

public class PhoneController
{
    private readonly PhonebookService _phonebookService = new PhonebookService();

    internal void ViewContacts()
    {
        var contacts = _phonebookService.GetAllContacts();
        Console.Clear();
        if (contacts.Count == 0)
            AnsiConsole.MarkupLine("[yellow]No contacts found![/]");
        else
            TableVisualisation.ShowTable(contacts);
    }

    internal void ViewContactsByFilter(string filter)
    {
        var filteredContacts = _phonebookService.GetContactsByCategory(filter);
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
        var contactIds = _phonebookService.GetContactsId();
        
        if (contactIds.Count == 0)
        {
            Util.AskUserToContinue();
            return;
        }

        int contactId = UserInputHelper.GetId(contactIds, "update");
        var contact = _phonebookService.GetContactById(contactId);

        if (updateName == true) contact[0].Name = UserInputHelper.GetName("update");
        if (updateEmail == true) contact[0].Email = UserInputHelper.GetEmail("update");
        if (updateNumber == true) contact[0].PhoneNumber = UserInputHelper.GetPhoneNumber("update");
        if (updateCategory == true) contact[0].Category = UserInputHelper.GetCategory("update");
        if (Util.ReturnToMenu()) return;
        
        _phonebookService.UpdateContact(contact[0]);
        
        AnsiConsole.MarkupLine("[yellow]Success! Updated contact.[/]");
        Util.AskUserToContinue();
    }
}