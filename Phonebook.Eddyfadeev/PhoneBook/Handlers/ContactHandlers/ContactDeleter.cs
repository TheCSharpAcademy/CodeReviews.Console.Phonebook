using PhoneBook.Interfaces.Handlers.ContactHandlers;
using PhoneBook.Interfaces.Repository;
using PhoneBook.Model;
using PhoneBook.Services;

namespace PhoneBook.Handlers.ContactHandlers;

/// <summary>
/// The ContactDeleter class handles the deletion of contacts from the repository.
/// </summary>
internal sealed class ContactDeleter : IContactDeleter
{
    private readonly IRepository<Contact> _contactRepository;

    public ContactDeleter(IRepository<Contact> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    /// <summary>
    /// Deletes a contact from the repository, prompting the user for confirmation.
    /// </summary>
    /// <param name="contact">The contact to be deleted.</param>
    /// <param name="message">An output parameter that will contain a message indicating the result of the delete operation.</param>
    public void DeleteContact(Contact contact, out string? message)
    {
        bool delete = PromptService.ConfirmAction(DeletePrompt);

        if (!delete)
        {
            message = default;
            return;
        }
        
        var result = _contactRepository.DeleteContact(contact);
        
        message = result switch
        {
            > 0 => ContactDeleted,
            0 => ContactWasNotDeleted,
            _ => default
        };
    }
}