using PhoneBook.Model;

namespace PhoneBook.Interfaces.Handlers.ContactHandlers;

/// <summary>
/// Interface for updating a contact in the repository.
/// </summary>
internal interface IContactUpdater
{
    /// <summary>
    /// Updates the specified contact and returns a status message indicating the result of the operation.
    /// </summary>
    /// <param name="contact">The contact to be updated.</param>
    /// <param name="message">An output parameter that holds the result message of the update operation.</param>
    void UpdateContact(Contact contact, out string? message);
}