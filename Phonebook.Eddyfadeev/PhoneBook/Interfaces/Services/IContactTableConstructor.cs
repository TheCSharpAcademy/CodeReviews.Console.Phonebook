using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Interfaces.Services;

/// <summary>
/// Interface for constructing a table representation of a contact.
/// </summary>
internal interface IContactTableConstructor
{
    /// <summary>
    /// Constructs a table representing the contact information.
    /// </summary>
    /// <param name="contact">The contact whose information is to be displayed in a table.</param>
    /// <returns>A table containing the contact's information.</returns>
    Table CreateContactTable(Contact contact);
}