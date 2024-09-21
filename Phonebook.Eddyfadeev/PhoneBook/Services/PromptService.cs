using PhoneBook.Enums;
using PhoneBook.Interfaces.Strategies;
using PhoneBook.Model;
using PhoneBook.Strategies.EditStrategies;
using Spectre.Console;

namespace PhoneBook.Services;

/// <summary>
/// The PromptService class provides methods for interacting with the user through prompts.
/// </summary>
internal static class PromptService
{
    private static readonly Dictionary<ContactEditOptions, IContactEditStrategy> EditStrategies = InitEditStrategies();

    /// <summary>
    /// Resolves the prompt strategy for the given contact based on the provided options.
    /// </summary>
    /// <param name="contact">The contact to be edited.</param>
    /// <param name="options">A collection of options specifying which aspects of the contact to edit.</param>
    public static void PromptStrategyResolver(Contact contact, params ContactEditOptions[] options)
    {
        foreach (var option in options)
        {
            if (EditStrategies.TryGetValue(option, out var strategy))
            {
                strategy.Edit(contact);
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]No strategy found for {option}[/]");
            }
        }
    }

    /// <summary>
    /// Prompts the user with a confirmation message and awaits a Yes/No response.
    /// </summary>
    /// <param name="prompt">The message to prompt the user with.</param>
    /// <returns>A boolean value indicating whether the user confirmed the action (true) or not (false).</returns>
    public static bool ConfirmAction(string prompt) => AnsiConsole.Confirm(prompt);

    /// <summary>
    /// Prompts the user for a message based on the provided question.
    /// </summary>
    /// <param name="whatToAsk">The message to display when asking for input.</param>
    /// <returns>The user's input as a string.</returns>
    public static string PromptForMessage(string whatToAsk)
    {
        var prompt = new TextPrompt<string>(whatToAsk)
        {
            AllowEmpty = true
        };
        
        return AnsiConsole.Prompt(prompt);
    }
    
    private static Dictionary<ContactEditOptions, IContactEditStrategy> InitEditStrategies() =>
        new()
        {
            { ContactEditOptions.FirstName, new FirstNameEditStrategy() },
            { ContactEditOptions.LastName, new LastNameEditStrategy() },
            { ContactEditOptions.Phone, new PhoneEditStrategy() },
            { ContactEditOptions.Email, new EmailEditStrategy() },
            { ContactEditOptions.Group, new GroupNameEditStrategy() }
        };
}