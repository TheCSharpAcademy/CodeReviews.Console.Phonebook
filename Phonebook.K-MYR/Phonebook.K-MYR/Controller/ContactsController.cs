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

    internal List<ContactDTO> GetAllContacts()
    {
        return _contactsService.GetAllContacts();
    }

    internal ContactDTO? GetContact()
    {
        return _contactsService.GetContact();
    }
}
