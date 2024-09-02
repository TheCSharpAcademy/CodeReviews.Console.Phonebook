using PhoneBook.Model;

namespace PhoneBook.Interfaces.Handlers;

/// <summary>
/// Interface for handling dynamic entries in a phone book application.
/// </summary>
internal interface IDynamicEntriesHandler
{
    /// <summary>
    /// Handles dynamic selection of contact entries based on the provided title and entries.
    /// </summary>
    /// <param name="title">The title to display in the selection prompt.</param>
    /// <param name="entries">The contact entries to display in the selection prompt.</param>
    /// <returns>Returns the selected contact entry.</returns>
    Contact HandleDynamicEntries(string title, params Contact[] entries);
}