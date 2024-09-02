using Spectre.Console;

namespace PhoneBook.Interfaces.Menu;

/// <summary>
/// Interface that defines methods for generating menu entries.
/// </summary>
internal interface IMenuEntries
{
    /// <summary>
    /// Generates a SelectionPrompt containing menu entries based on the provided enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type representing the menu options.</typeparam>
    /// <param name="title">The title to be displayed at the top of the menu prompt.</param>
    /// <returns>A SelectionPrompt containing the menu entries derived from the enum type.</returns>
    SelectionPrompt<string> GetMenuEntries<TEnum>(string title)
        where TEnum : struct, Enum;

    /// <summary>
    /// Generates a multi-selection prompt allowing the user to select multiple entries based on the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type whose descriptions will be used as choices in the prompt.</typeparam>
    /// <param name="title">The title of the multi-selection prompt.</param>
    /// <returns>A <see cref="MultiSelectionPrompt{String}"/> configured with options corresponding to the enum descriptions.</returns>
    public MultiSelectionPrompt<string> GetSelectableMenuEntries<TEnum>(string title)
        where TEnum : struct, Enum;
}