using PhoneBook.Model;

namespace PhoneBook.Interfaces.Handlers.ContactHandlers;

/// <summary>
/// Handles the deletion of contact entries.
/// </summary>
internal interface IContactDeleter
{
    /// <summary>
    /// Deletes a contact from the repository, prompting the user for confirmation.
    /// </summary>
    /// <param name="contact">The contact to be deleted.</param>
    /// <param name="message">An output parameter that will contain a message indicating the result of the delete operation.</param>
    void DeleteContact(Contact contact, out string? message);
}