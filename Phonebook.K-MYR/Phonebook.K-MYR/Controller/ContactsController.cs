using Phonebook.K_MYR.Models;

namespace Phonebook.K_MYR;

internal class ContactsController
{
    private readonly ContactsService _contactsService;

    public ContactsController(ContactsService contactsService)
    {
        _contactsService = contactsService;
    }
    
    internal void AddContact()
    {
        _contactsService.AddContact();
    }

    internal void DeleteContact()
    {
        _contactsService.DeleteContact();

    }

    internal void UpdateContact()
    {
        _contactsService.UpdateContact();
    }

    internal IEnumerable<Contact> GetAllContacts()
    {
        return _contactsService.GetAllContacts();
    }

    internal Contact GetContact()
    {
        return _contactsService.GetContact();
    }
}
