namespace PhoneBook.Interfaces.Handlers.ContactHandlers;

/// <summary>
/// Handles adding of contact entries.
/// </summary>
internal interface IContactAdder
{
    /// <summary>
    /// Adds a new contact to the repository.
    /// </summary>
    /// <param name="message">Output message indicating the success or failure of the add operation.</param>
    void AddContact(out string? message);
}