namespace PhoneBook.Console.Views;

using Spectre.Console;

public static class Helper
{
    public static string AskFullName(string prompt)
    {
        return AnsiConsole.Ask<string>(prompt);
    }

    public static string AskEmail(string prompt)
    {
        return AnsiConsole.Prompt(
                 new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage("Email is not in proper form ex info@example.com")
                .Validate(validEmail => Validator.IsValidEmail(validEmail)));
    }

    public static string AskPhone(string prompt)
    {
        return AnsiConsole.Prompt(
                new TextPrompt<string>(prompt)
                .PromptStyle("green")
                .ValidationErrorMessage("Phone number must be in (555)555-5555 or 555-555-5555 format")
                .Validate(validPhone => Validator.IsValidPhone(validPhone)));
    }

    public static bool Confirm(string prompt)
    {
        return AnsiConsole.Confirm(prompt);
    }
}
