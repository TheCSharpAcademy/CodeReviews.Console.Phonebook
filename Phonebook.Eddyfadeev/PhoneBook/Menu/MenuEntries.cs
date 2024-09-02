using PhoneBook.Extensions;
using PhoneBook.Interfaces.Menu;
using Spectre.Console;

namespace PhoneBook.Menu;

/// <summary>
/// MenuEntries class used to generate menu entries in selection and multi-selection prompts
/// based on the provided enum type.
/// </summary>
internal class MenuEntries : IMenuEntries
{
    /// <summary>
    /// Generates a SelectionPrompt filled with menu entries derived from the given enum.
    /// </summary>
    /// <typeparam name="TEnum">An enum representing the menu choices.</typeparam>
    /// <param name="title">The title of the SelectionPrompt.</param>
    /// <returns>A fully constructed SelectionPrompt with the provided title and choices.</returns>
    public SelectionPrompt<string> GetMenuEntries<TEnum>(string title) 
        where TEnum : struct, Enum =>
        new SelectionPrompt<string>()
            .Title(title)
            .AddChoices(GetDescriptions<TEnum>());

    /// <summary>
    /// Generates a multi-selection prompt with menu entries based on the provided enumeration type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to be used for generating menu entries.</typeparam>
    /// <param name="title">The title displayed at the top of the selection prompt.</param>
    /// <returns>A <see cref="MultiSelectionPrompt{String}"/> object populated with the menu entries.</returns>
    public MultiSelectionPrompt<string> GetSelectableMenuEntries<TEnum>(string title)
        where TEnum : struct, Enum =>
        new MultiSelectionPrompt<string>()
            .Title(title)
            .NotRequired()
            .AddChoices(GetDescriptions<TEnum>());

    private static string[] GetDescriptions<TEnum>()
        where TEnum : struct, Enum =>
        Enum.GetValues<TEnum>()
            .Select(EnumExtensions.GetDescription)
            .ToArray();
}