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

    internal void ViewAllContacts()
    {
        var contacts = _contactsService.GetContacts();
    }

    internal void ViewContact()
    {
        var contact =_contactsService.GetContactInput();
    }
}
