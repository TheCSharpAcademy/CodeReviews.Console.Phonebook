using System.ComponentModel.DataAnnotations;
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

    public void AddContact(IEnumerable<ContactCategory> contactCategories)
    {
        var contact = UserInput.GetNewContact(contactCategories);
        if (contact == null)
        {
            AnsiConsole.Markup("You canceled the Operation\n");
            VisualizationEngine.DisplayContinueMessage();
            return;
        }
        _contactRepository.AddContact(contact);
        AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Added [green]Successfully[/]");
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void UpdateContact(IEnumerable<ContactCategory> contactCategories)
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        // Validation goes here
        var contact = contacts.FirstOrDefault(x => x.Id == id);
        if (contact == null)
        {
            AnsiConsole.Markup($"Contact with id {id} not found!");
            Console.ReadLine();
            return;
        }
        if (UserInput.UpdateContact(contact, contactCategories))
        {
            _contactRepository.UpdateContact(contact);
            AnsiConsole.Markup($"Contact {contact.Name} {contact.Email} {contact.PhoneNumber} Updated [green]Successfully[/]\n");
        }
        else
        {
            AnsiConsole.Markup("You canceled the Operation\n");
        }
        VisualizationEngine.DisplayContinueMessage();
    }

    internal void DeleteContact()
    {
        var contacts = _contactRepository.GetAllContacts();
        VisualizationEngine.DisplayContacts(contacts, "Contacts Table");
        var id = UserInput.GetIntInput();
        var contact = _contactRepository.GetContactById(id);
        if (contact == null)
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