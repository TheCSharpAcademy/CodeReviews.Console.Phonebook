using PhoneBook.Model;

namespace PhoneBook.Interfaces.Handlers.ContactHandlers;

/// <summary>
/// Interface for selecting a contact.
/// </summary>
internal interface IContactSelector
{
    /// <summary>
    /// Selects a contact from the repository and returns it along with a message.
    /// </summary>
    /// <param name="contact">The selected contact, or <c>null</c> if no contacts are available.</param>
    /// <param name="message">A message indicating the outcome of the contact selection.</param>
    void SelectContact(out Contact? contact, out string message);
}