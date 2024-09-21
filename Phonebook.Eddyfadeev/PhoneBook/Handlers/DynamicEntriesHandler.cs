using PhoneBook.Interfaces.Handlers;
using PhoneBook.Model;
using Spectre.Console;

namespace PhoneBook.Handlers;

/// <summary>
/// A handler class for managing dynamic entries in a contact-based phone book application.
/// </summary>
internal class DynamicEntriesHandler : IDynamicEntriesHandler
{
    /// <summary>
    /// Handles dynamic selection of contact entries based on the provided title and entries.
    /// </summary>
    /// <param name="title">The title to display in the selection prompt.</param>
    /// <param name="entries">The contact entries to display in the selection prompt.</param>
    /// <returns>Returns the selected contact entry.</returns>
    public Contact HandleDynamicEntries(string title, params Contact[] entries) =>
        AnsiConsole.Prompt(GetSelectionPrompt(title, entries));
    
    private static SelectionPrompt<Contact> GetSelectionPrompt(string title, params Contact[] entries)
    {
        const int maxCountPerPage = 4;
        var selectionPrompt = new SelectionPrompt<Contact>()
            .Title(title);
        
        selectionPrompt.AddChoices(entries);

        selectionPrompt.PageSize(maxCountPerPage);
        return selectionPrompt;
    }
}