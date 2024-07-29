using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Phonebook.ConsoleApp.Enums;
using Phonebook.ConsoleApp.Models;
using Phonebook.Data.Entities;
using Phonebook.Extensions;
using PhoneNumbers;
using Spectre.Console;

namespace Phonebook.ConsoleApp.Services;

/// <summary>
/// Helper service for getting valid user inputs of different types.
/// </summary>
internal static partial class UserInputService
{
    #region Constants

    private readonly static string EmailValidationErrorMessage = "[red]Invalid email address![/] Expected format: [blue]user@domain.tld[/]";

    private readonly static string PhoneValidationErrorMessage = "[red]Invalid phone number[/] Enter according to your regional standard.";

    #endregion
    #region Methods - Internal

    internal static string GetString(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            );
    }

    internal static string GetString(string prompt, string defaultValue)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .DefaultValue(defaultValue)
            );
    }

    internal static string GetEmailAddress(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .ValidationErrorMessage(EmailValidationErrorMessage)
            .Validate(input =>
            {
                return (input == "0" || new EmailAddressAttribute().IsValid(input))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));
    }

    internal static string GetEmailAddress(string prompt, string defaultValue)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .DefaultValue(defaultValue)
            .ValidationErrorMessage(EmailValidationErrorMessage)
            .Validate(input =>
            {
                return (input == "0" || new EmailAddressAttribute().IsValid(input))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));
    }

    internal static string GetPhoneNumber(string prompt)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .ValidationErrorMessage(PhoneValidationErrorMessage)
            .Validate(input =>
            {
                return (input == "0" || IsValidPhoneNumber(input))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));
    }

    internal static string GetPhoneNumber(string prompt, string defaultValue)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("blue")
            .DefaultValue(defaultValue)
            .ValidationErrorMessage(PhoneValidationErrorMessage)
            .Validate(input =>
            {
                return (input == "0" || IsValidPhoneNumber(input))
                ? Spectre.Console.ValidationResult.Success()
                : Spectre.Console.ValidationResult.Error();
            }));
    }

    internal static Category GetCategory(string prompt, IReadOnlyList<Category> categories)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<Category>()
                .Title(prompt)
                .AddChoices(categories)
                .UseConverter(c => c.Name)
                );
    }

    internal static PageChoice GetPageChoice(string prompt, IEnumerable<PageChoice> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<PageChoice>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.Name)
                );
    }

    internal static PageChoices GetPageChoiceEnum(string prompt, IEnumerable<PageChoices> choices)
    {
        return AnsiConsole.Prompt(
                new SelectionPrompt<PageChoices>()
                .Title(prompt)
                .AddChoices(choices)
                .UseConverter(c => c.GetDescription())
                );
    }

    #endregion
    #region Methods - Private

    private static bool IsValidPhoneNumber(string input)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        try
        {
            var phoneNumber = phoneNumberUtil.Parse(input, RegionInfo.CurrentRegion.Name);
            return phoneNumberUtil.IsValidNumber(phoneNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    #endregion
}
