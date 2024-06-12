using System.ComponentModel.DataAnnotations;
using Drinks;
using Phonebook.Data;
using Phonebook.Entities;
using Phonebook.Repositories;
using Spectre.Console;
using Phonebook.Validators;

namespace Phonebook.Services;

public class ContactService
{
    private ContactRepository _contactRepository;

    public ContactService(PhonebookDbContext context)
    {
        _contactRepository = new ContactRepository(context);
    }

    public void AddContact()
    {
        var contact = UserInput.GetNewContact();
        _contactRepository.AddContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Added [green]Successfully[/]");
        Console.ReadLine();
    }

    internal void UpdateContact()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        // Validation goes here
        var contact = contacts.FirstOrDefault(x => x.Id == id);
        if(contact == null)
        {
            AnsiConsole.Markup($"Contact with id {id} not found!");
            Console.ReadLine();
            return;
        }
        UserInput.UpdateContact(contact);
        _contactRepository.UpdateContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Updated [green]Successfully[/]");
        Console.ReadLine();
    }

    internal void DeleteContact()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        var contact = _contactRepository.GetContactById(id);
        if(contact == null)
        {
            AnsiConsole.Markup($"Contact with id [maroon]{id}[/] not found!");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        _contactRepository.DeleteContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Updated [green]Successfully[/]");
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void ViewAllContacts()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        VisualizationEngine.DisplayContinueMessage();
    }

}