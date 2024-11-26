using Spectre.Console;

namespace Phonebook.Utilities;

public static class UserInputHelper
{
    internal static string GetName(string text)
    {
        var contactName = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]name[/] of contact to {text}:")
                .Validate(input =>
                        Validator.IsNameValid(input)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Name cannot contain numbers.[/]")));
        return contactName;
    }

    internal static string GetEmail(string text)
    {
        var contactEmail = AnsiConsole.Prompt(
                new TextPrompt<string>($"Enter [green]email[/] of contact to {text}:")
                    .Validate(input =>
                        Validator.IsEmailValid(input)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Invalid Email. Email expected format 'JohnDoe@example.com'[/]")
                        ));
        return contactEmail;
    }

    internal static string GetPhoneNumber(string text)
    {
        var contactNumber = AnsiConsole.Prompt(
            new TextPrompt<string>($"Enter [green]phone number[/] of contact to {text}:")
                .Validate(input =>
                    Validator.IsPhoneNumberValid(input)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid Phone Number. Phone number expected format '+44123456789'. One '+' sign and then 11 numbers[/]")));
        return contactNumber;
    }

    internal static string GetCategory(string text)
    {
        var contactCategory = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter [green]category[/] of contact to {text}: (Friends/Family/Work)")
                    .Validate(input =>
                        Validator.IsCategoryValid(input)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Invalid Category. Enter (Family/Friends/Work)[/]")));
        return Util.Capitalise(contactCategory);
    }

    internal static int GetId(List<int> ids, string text)
    {
        var idPresent = AnsiConsole.Prompt(
            new TextPrompt<int>($"Enter [green]id[/] of contact to [cyan]{text}[/]:")
                .Validate(input =>
                    ids.Contains(Convert.ToInt32(input))
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Id doesn't exist.[/]")));
        return idPresent;
    }
}