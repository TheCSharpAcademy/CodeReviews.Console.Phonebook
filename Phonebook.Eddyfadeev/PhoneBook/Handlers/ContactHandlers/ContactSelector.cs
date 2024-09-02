using PhoneBook.Interfaces.Handlers;
using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Repository;
using PhoneBook.Model;

namespace PhoneBook.Handlers.ContactHandlers;

/// <summary>
/// Handles the selection of a contact from the repository.
/// </summary>
internal class ContactSelector : IContactSelector
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly IDynamicEntriesHandler _dynamicEntriesHandler;

    public ContactSelector(IRepository<Contact> contactRepository, IDynamicEntriesHandler dynamicEntriesHandler)
    {
        _contactRepository = contactRepository;
        _dynamicEntriesHandler = dynamicEntriesHandler;
    }

    /// <summary>
    /// Selects a contact from the repository and returns it along with a message.
    /// </summary>
    /// <param name="contact">The selected contact, or <c>null</c> if no contacts are available.</param>
    /// <param name="message">A message indicating the outcome of the contact selection.</param>
    public void SelectContact(out Contact? contact, out string message)
    {
        var contacts = _contactRepository.GetAllContacts();
        if (contacts.Length == 0)
        {
            message = NoContactsInTheDatabase;
            contact = default;
            return;
        }
        
        var selectedContact = 
            _dynamicEntriesHandler.HandleDynamicEntries(ContactsHandlerTitle, contacts);

        contact = selectedContact;

        message = ContactRetrieved;
    }
}